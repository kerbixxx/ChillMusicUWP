using ChillMusicUWP.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillMusicUWP.MVVM.ViewModel
{
    public class SongPageViewModel : ObservableObject
    {
        public Song CurrentSong { get; set; }
        public SongPageViewModel()
        {

        }
    }
}
