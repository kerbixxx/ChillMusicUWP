using ChillMusicUWP.Data.Repositories;
using ChillMusicUWP.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
