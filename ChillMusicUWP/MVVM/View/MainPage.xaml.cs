using ChillMusicUWP.MVVM.Model;
using ChillMusicUWP.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Способ привязать View и ViewModel был взят отсюда https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/puttingthingstogether
// Раздел Binding the UI

namespace ChillMusicUWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ServiceProvider serviceProvider = App.GetServiceProvider();

            this.DataContext = serviceProvider.GetService<MainPageViewModel>();
        }

        public MainPageViewModel ViewModel => (MainPageViewModel)this.DataContext;
    }
}
