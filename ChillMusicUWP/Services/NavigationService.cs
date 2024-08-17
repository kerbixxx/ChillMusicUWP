using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ChillMusicUWP.Services
{
    public static class NavigationService
    {
        private static Frame _rootFrame;

        public static void SetRootFrame(Frame rootFrame)
        {
            _rootFrame = rootFrame;
        }

        public static void NavigateToPage(Type type, object parameter = null)
        {
            if (_rootFrame == null)
            {
                throw new InvalidOperationException("Root frame has not been set.");
            }
            _rootFrame.Navigate(type, parameter);
        }
    }
}