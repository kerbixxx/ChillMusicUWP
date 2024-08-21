using ChillMusicUWP.Interfaces;
using ChillMusicUWP.MVVM.Model;
using System;
using System.Collections.Generic;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace ChillMusicUWP.Services
{
    public class PlaybackService : IPlaybackService
    {
        private Dictionary<string, MediaPlayer> _players = new Dictionary<string, MediaPlayer>();


        public void PlaySong(Song song)
        {
            if (!_players.ContainsKey(song.Name))
            {
                var player = new MediaPlayer();
                player.MediaEnded += MediaPlayer_MediaEnded;
                player.Source = MediaSource.CreateFromUri(new Uri($"ms-appx://{song.SongFile}"));
                _players[song.Name] = player;
            }

            _players[song.Name].Play();
        }

        public void AddEffect(Sound sound)
        {
            if (!_players.ContainsKey(sound.Name))
            {
                var player = new MediaPlayer();
                player.MediaEnded += MediaPlayer_MediaEnded;
                player.Source = MediaSource.CreateFromUri(new Uri($"ms-appx://{sound.SoundFile}"));
                _players[sound.Name] = player;
            }

            _players[sound.Name].Play();
        }

        private void MediaPlayer_MediaEnded(MediaPlayer sender, object e)
        {
            sender.Play();
        }

        public void StopPlayer()
        {
            foreach (var player in _players.Values)
            {
                player.Pause();
                player.Dispose();
            }
            _players.Clear();
        }
        public void StopPlaying(string name)
        {
            if (_players.TryGetValue(name, out var player))
            {
                player.Pause();
                player.Dispose();
            }
            _players.Remove(name);
        }

        public void PausePlaying()
        {
            foreach(var player in _players.Values)
            {
                player.Pause();
            }
        }

        public void ResumePlaying()
        {
            foreach (var player in _players.Values)
            {
                player.Play();
            }
        }

        public void SetVolume(string name, double volume)
        {
            if (_players.TryGetValue(name, out var player))
            {
                player.Volume = volume;
            }
        }
    }
}