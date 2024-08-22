using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ChillMusicUWP.Converters
{
    public class BooleanToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isPlaying)
            {
                return isPlaying ? "/Assets/PauseIcon.png" : "/Assets/PlayIcon.png";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value.ToString() == "/Assets/PauseIcon.png" ? true : false;
        }

    }
}
