using ChillMusicUWP.Data.Repositories;
using ChillMusicUWP.Interfaces;
using ChillMusicUWP.MVVM.Model;
using ChillMusicUWP.MVVM.View;
using ChillMusicUWP.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.Xaml;

namespace ChillMusicUWP.MVVM.ViewModel
{
    public partial class SongPageViewModel : ObservableObject
    {
        private readonly IRepository<Sound> _soundRepository;
        private readonly IPlaybackService _playbackService;
        private bool IsPlaying = true;
        private DispatcherTimer _timer;
        private int _remainingSeconds;
        private ObservableCollection<Sound> Sounds { get; set; }
        private ObservableCollection<Sound> SelectedSounds { get; set; }
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
            Sounds = new ObservableCollection<Sound>(_soundRepository.GetAllAsync().GetAwaiter().GetResult());
        }

        [RelayCommand]
        void NavigateToMain()
        {
            IsPopupOpen = false;
            _playbackService.StopPlayer();
            NavigationService.NavigateToPage(typeof(MainPage));
        }

        [RelayCommand]
        void PauseAudio()
        {
            if (IsPlaying) _playbackService.PausePlaying();
            else _playbackService.ResumePlaying();
            IsPlaying = !IsPlaying;
        }

        #region Таймер
        [RelayCommand]
        void OpenTimerPopup()
        {
            IsPopupTimerOpen = true;
        }
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
            }
        }

        private void UpdateDisplay()
        {
            ButtonContent = $"{_remainingSeconds} секунд";
        }

        private string _buttonContent;
        public string ButtonContent
        {
            get => _buttonContent;
            set => SetProperty(ref _buttonContent, value);
        }
        #endregion

        [RelayCommand]
        void AddEffect(Sound sound)
        {
            _playbackService.AddEffect(sound);
        }

        public void PlaySong()
        {
            _playbackService.PlaySong(CurrentSong);
        }

        private bool _isPopupOpen;
        public bool IsPopupOpen
        {
            get => _isPopupOpen;
            set => SetProperty(ref _isPopupOpen, value);
        }
        [RelayCommand]
        void OpenPopup()
        {
            if(!IsPopupOpen) IsPopupOpen = true;
        }
        private bool _isPopupTimerOpen;
        public bool IsPopupTimerOpen
        {
            get => _isPopupTimerOpen;
            set => SetProperty(ref _isPopupTimerOpen, value);
        }

        [RelayCommand]
        void Test()
        {
            Console.WriteLine("heh");
        }
    }
}