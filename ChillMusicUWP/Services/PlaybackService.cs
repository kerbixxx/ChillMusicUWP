using ChillMusicUWP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.Render;
using Windows.Storage;
using Windows.UI.Xaml;

namespace ChillMusicUWP.Services
{
    public class PlaybackService
    {
        private MediaPlayer _mediaPlayer;
        private DispatcherTimer _timer;
        public PlaybackService()
        {
            _mediaPlayer = new();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        public void PlaySongAsync(Song song)
        {
            _mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx://{song.SongFile}"));
            _mediaPlayer.Play();
            _timer.Start();
        }
        private void Timer_Tick(object sender, object e)
        {
            if (_mediaPlayer.Position == _mediaPlayer.NaturalDuration)
            {
                _timer.Stop();
                _mediaPlayer.Play();
            }
        }
        public void StopPlaying()
        {
            _mediaPlayer.Pause();
            _mediaPlayer.Source = null;
        }

        public void PausePlaying()
        {
            _mediaPlayer.Pause();
        }

        public void ResumePlaying()
        {
            _mediaPlayer.Play();
        }
        public void AddEffect(Sound sound)
        {

        }
    }
}