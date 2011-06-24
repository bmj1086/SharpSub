using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;

namespace SharpSub.Data
{
    public class SongPlayer : IDisposable
    {
        public WindowsMediaPlayer player = new WindowsMediaPlayer();
        public WMPPlayState PlaybackState = WMPPlayState.wmppsStopped;
        public int Volume
        {
            get { return player.settings.volume; }
            set { player.settings.volume = value; }
        }
        public double Position
        {
            get { return player.controls.currentPosition; }
            set { player.controls.currentPosition = value; }
        }
        public bool Mute
        {
            get { return player.settings.mute; }
            set { player.settings.mute = value; }
        }

        public string DurationString
        {
            get { return player.currentMedia.durationString; }
        }

        public void Play()
        {
            player.controls.play();
        }

        public SongPlayer(Song song)
        {
            player.URL = song.Url;
            player.PlayStateChange += player_PlayStateChange;
        }

        void player_PlayStateChange(int NewState)
        {
            PlaybackState = (WMPPlayState) NewState;
        }

        public void Stop()
        {
            if (PlaybackState != WMPPlayState.wmppsStopped)
                player.controls.stop();
        }

        public void Pause()
        {
            if (PlaybackState == WMPPlayState.wmppsPlaying)
                player.controls.pause();
        }

        public void Resume()
        {
            if (PlaybackState == WMPPlayState.wmppsPaused)
                player.controls.play();
        }


        public void Dispose()
        {
            player.controls.stop();
            player = null;
        }
    }
}
