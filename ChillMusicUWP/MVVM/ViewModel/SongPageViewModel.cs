using ChillMusicUWP.Data.Repositories;
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

namespace ChillMusicUWP.MVVM.ViewModel
{
    public partial class SongPageViewModel : ObservableObject
    {
        private readonly IRepository<Sound> _soundRepository;
        private readonly PlaybackService _playbackService;
        private bool IsPlaying = true;
        private ObservableCollection<Sound> Sounds { get; set; }
        public Song CurrentSong { get; set; }
        public SongPageViewModel(PlaybackService playbackService, IRepository<Sound> soundRepository)
        {
            _playbackService = playbackService;
            _soundRepository = soundRepository;

            InitializeSounds();
        }

        private void InitializeSounds()
        {
            var soundsFromDb = _soundRepository.GetAllAsync().GetAwaiter().GetResult();
            Sounds = new ObservableCollection<Sound>(soundsFromDb);
        }

        [RelayCommand]
        void NavigateToMain()
        {
            _playbackService.StopPlaying();
            NavigationService.NavigateToPage(typeof(MainPage));
        }

        [RelayCommand]
        void PauseSong()
        {
            if (IsPlaying) _playbackService.PausePlaying();
            else _playbackService.ResumePlaying();
            IsPlaying = !IsPlaying;
        }

        [RelayCommand]
        void SetTimer()
        {

        }

        public void PlaySong()
        {
            _playbackService.PlaySongAsync(CurrentSong);
        }
    }
}
