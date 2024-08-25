using ChillMusicUWP.Data.Repositories;
using ChillMusicUWP.Interfaces;
using ChillMusicUWP.MVVM.Model;
using ChillMusicUWP.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;

namespace ChillMusicUWP.MVVM.ViewModel
{
    public partial class SongPageViewModel : ObservableObject
    {
        private readonly IRepository<Sound> _soundRepository;
        private readonly IPlaybackService _playbackService;
        private DispatcherTimer _timer;
        private int _remainingSeconds;
        public Dictionary<string, List<Sound>> GroupedSounds { get; private set; } = new();
        public ObservableCollection<Sound> SelectedSounds { get; set; } = new();
        public Song CurrentSong { get; set; }
        public SongPageViewModel(IRepository<Sound> soundRepository, IPlaybackService playbackService)
        {
            ButtonContent = "Таймер";
            _playbackService = playbackService;
            _soundRepository = soundRepository;
            InitializeSounds();
        }
        private void InitializeSounds()
        {
            var sounds = _soundRepository.GetAllAsync().GetAwaiter().GetResult();

            var orderedGroups = from sound in sounds
                                group sound by sound.SoundCategory.Name into newGroup
                                orderby newGroup.Key
                                select newGroup;

            foreach (var group in orderedGroups)
            {
                if (!GroupedSounds.ContainsKey(group.Key))
                {
                    GroupedSounds[group.Key] = new List<Sound>();
                }
                foreach (var sound in group)
                {
                    GroupedSounds[group.Key].Add(sound);
                }
            }
        }

        #region Команды через view
        public void SetVolume(string name, double volume)
        {
            _playbackService.SetVolume(name, volume);
        }
        public void NavigateToMain()
        {
            IsPopupTimerOpen = false;
            IsPopupOpen = false;
            IsPlaying = false;
            _playbackService.StopPlayer();
            NavigationService.NavigateToPage(typeof(MainPage));
            SelectedSounds = new();
        }

        public void PauseAudio()
        {
            if (IsPlaying) _playbackService.PausePlaying();
            else _playbackService.ResumePlaying();
            IsPlaying = !IsPlaying;
        }

        public void OpenTimerPopup()
        {
            if(!IsPopupTimerOpen && CanClickTimerButton) IsPopupTimerOpen = true;
        }
        public void PlaySong()
        {
            _playbackService.PlaySong(CurrentSong);
            IsPlaying = true;
        }
        #endregion
        #region Таймер
        [RelayCommand]
        void CloseTimerPopup()
        {
            IsPopupTimerOpen = false;
        }
        [RelayCommand]
        void SetTimer(string seconds)
        {
            _remainingSeconds = int.Parse(seconds);
            UpdateDisplay();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            IsPopupTimerOpen = false;
            CanClickTimerButton = false;
        }
        private void Timer_Tick(object sender, object e)
        {
            _remainingSeconds--;
            UpdateDisplay();
            if (_remainingSeconds <= 0)
            {
                _playbackService.PausePlaying();
                _timer.Stop();
                IsPlaying = false;
                ButtonContent = "Таймер";
                CanClickTimerButton = true;
            }
        }

        private void UpdateDisplay()
        {
            ButtonContent = $"{_remainingSeconds} секунд";
        }
        #endregion
        #region RelayCommands
        [RelayCommand]
        public void AddEffect(Sound sound)
        {
            if (!SelectedSounds.Contains(sound))
            {
                _playbackService.AddEffect(sound);
                SelectedSounds.Add(sound);
                IsPopupOpen = false;
            }
        }

        [RelayCommand]
        void ManageSoundControlPopup()
        {
            IsSoundControlPopupOpen = !IsSoundControlPopupOpen;
        }

        [RelayCommand]
        void RemoveEffect(Sound sound)
        {
            _playbackService.StopPlaying(sound.Name);
            SelectedSounds.Remove(sound);
        }

        [RelayCommand]
        void OpenPopup()
        {
            IsPopupOpen = !IsPopupOpen;
        }
        #endregion
        #region Variables
        private bool _isPopupTimerOpen;
        public bool IsPopupTimerOpen
        {
            get => _isPopupTimerOpen;
            set => SetProperty(ref _isPopupTimerOpen, value);
        }

        private bool _canClickTimerButton = true;
        public bool CanClickTimerButton
        {
            get => _canClickTimerButton;
            set => SetProperty(ref _canClickTimerButton, value);
        }

        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set => SetProperty(ref _buttonContent, value);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }

        private bool _isPlaying = true;
        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        private bool _isSoundControlPopupOpen;
        public bool IsSoundControlPopupOpen
        {
            get => _isSoundControlPopupOpen;
            set => SetProperty(ref _isSoundControlPopupOpen, value);
        }
        #endregion
    }
}