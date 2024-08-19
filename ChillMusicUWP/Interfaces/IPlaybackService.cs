using ChillMusicUWP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChillMusicUWP.Interfaces
{
    public interface IPlaybackService
    {
        void PlaySong(Song song);
        void StopPlaying();
        void PausePlaying();
        void ResumePlaying();
        void AddEffect(Sound sound);
    }
}
