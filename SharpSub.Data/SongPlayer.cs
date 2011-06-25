using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMPLib;

namespace SharpSub.Data
{
    public class SongPlayer : IDisposable
    {
        private WindowsMediaPlayer player = new WindowsMediaPlayer();
        public WMPPlayState PlaybackState = WMPPlayState.wmppsStopped;
        public Song CurrentSong;

        public SongPlayer(Song song)
        {
            CurrentSong = song;
            player.URL = song.Url;
            player.PlayStateChange += PlayerPlayStateChange;
        }

        static void PlayerMediaError(object pMediaObject)
        {
            
        }
        
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
        public string PositionString(string format = "mm:ss")
        {
            return ToTimeFormat((int) Position, format);
        }
        public bool Mute
        {
            get { return player.settings.mute; }
            set { player.settings.mute = value; }
        }
        public string DurationString(string format = "mm:ss")
        {
            return ToTimeFormat((int) CurrentSong.Duration, format);
        }

        private string ToTimeFormat(int seconds, string format)
        {
            DateTime dt = DateTime.MinValue.AddSeconds(seconds);
            return dt.ToString("m:ss");
        }

        public void Play()
        {
            player.controls.play();
        }


        void PlayerPlayStateChange(int newState)
        {
            PlaybackState = (WMPPlayState) newState;
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
