using ChillMusicUWP.MVVM.Model;
using ChillMusicUWP.MVVM.ViewModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
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
                Song = selectedSong;
                ViewModel.CurrentSong = Song;
                ViewModel.PlaySong();
            }
            base.OnNavigatedTo(e);
        }

        private void PlayPauseButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.PauseAudio();
        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.NavigateToMain();
        }

        private void TimerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.OpenTimerPopup();
        }

        private void SoundTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            if (textBlock.Tag is Sound sound)
            {
                ViewModel.AddEffect(sound);
            }
        }

        private void volumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = (Slider)sender;
            string name = slider.Tag as string;
            if (name != null)
            {
                double value = slider.Value/100;
                ViewModel.SetVolume(name, value);
            }
        }
    }
}
