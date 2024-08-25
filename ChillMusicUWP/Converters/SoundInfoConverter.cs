using ChillMusicUWP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace ChillMusicUWP.Converters
{
    public class SoundInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Tuple<string, double>)
            {
                var tuple = (Tuple<string, double>)value;
                return new AudioInfo
                {
                    Name = tuple.Item1,
                    Volume = tuple.Item2
                };
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
