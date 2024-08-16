using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ChillMusicUWP.MVVM.Model
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SongFile { get; set; }
        public BitmapImage Image { get; set; }
    }
}
