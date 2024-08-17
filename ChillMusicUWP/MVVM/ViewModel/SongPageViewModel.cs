using ChillMusicUWP.MVVM.Model;
using ChillMusicUWP.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillMusicUWP.MVVM.ViewModel
{
    public partial class SongPageViewModel : ObservableObject
    {
        private readonly PlaybackService playbackService;
        private bool IsPlaying = true;
        public Song CurrentSong { get; set; }
        public SongPageViewModel(PlaybackService playbackService)
        {
            this.playbackService = playbackService;
        }
        [RelayCommand]
        void NavigateToMain()
        {
            playbackService.StopPlaying();
            NavigationService.NavigateToPage(typeof(MainPage));
        }
        [RelayCommand]
        void PauseSong()
        {
            if (IsPlaying) playbackService.PausePlaying();
            else playbackService.ResumePlaying();
            IsPlaying = !IsPlaying;
        }
        public void PlaySong()
        {
            playbackService.PlaySongAsync(CurrentSong);
        }
    }
}
