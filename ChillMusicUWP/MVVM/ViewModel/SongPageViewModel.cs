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
    public partial class SongPageViewModel : ObservableObject, INotifyPropertyChanged
    {
        public Song CurrentSong { get; set; }
        public SongPageViewModel() { }
        [RelayCommand]
        void NavigateToMain()
        {
            NavigationService.NavigateToPage(typeof(MainPage));
        }
    }
}
