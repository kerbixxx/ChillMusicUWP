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
        private List<Song> _songs;
        [ObservableProperty]
        private List<Song> songs;
        public MainPageViewModel() { }
    }
}
