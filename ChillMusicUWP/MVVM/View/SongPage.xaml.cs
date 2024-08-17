using ChillMusicUWP.MVVM.Model;
using ChillMusicUWP.MVVM.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
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

namespace ChillMusicUWP.MVVM.View
{
    public sealed partial class SongPage : Page
    {
        public Song Song { get; set; }

        public SongPage()
        {
            this.InitializeComponent();
            ServiceProvider serviceProvider = App.GetServiceProvider();

            this.DataContext = serviceProvider.GetService<SongPageViewModel>();
        }

        public SongPageViewModel ViewModel => (SongPageViewModel)this.DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is Song selectedSong)
            {
                Song = e.Parameter as Song;
            }
            base.OnNavigatedTo(e);
        }
    }
}
