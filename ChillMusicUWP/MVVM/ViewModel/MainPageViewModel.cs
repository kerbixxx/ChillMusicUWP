using ChillMusicUWP.Data.Repositories;
using ChillMusicUWP.MVVM.Model;
using ChillMusicUWP.MVVM.View;
using ChillMusicUWP.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace ChillMusicUWP.MVVM.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IRepository<Song> _songRepository;
        public ObservableCollection<Song> Songs { get; set; }
        public MainPageViewModel(IRepository<Song> songRepository)
        {
            _songRepository = songRepository;

            InitializeSongs();
        }

        private void InitializeSongs()
        {
            var songsFromDb = _songRepository.GetAllAsync().Result;
            Songs = new ObservableCollection<Song>(songsFromDb);
        }

        [RelayCommand]
        void OpenSong(Song song)
        {
            NavigationService.NavigateToPageAsync(typeof(SongPage), song);
        }
    }
}
