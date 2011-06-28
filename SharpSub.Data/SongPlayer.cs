using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using WMPLib;

namespace SharpSub.Data
{
    public class SongPlayer : IDisposable
    {
        private WindowsMediaPlayer player = new WindowsMediaPlayer();
        public PlaybackState State = PlaybackState.Stopped;
        public IList<Song> CurrentPlaylist;

        public event _WMPOCXEvents_CurrentItemChangeEventHandler CurrentItemChanged
        {
            add { player.CurrentItemChange += value; }
            remove { player.CurrentItemChange -= value; }
        }
        
        public Song CurrentSong
        {
            get { return CurrentPlaylist.Where(s => s.Url == player.controls.currentItem.sourceURL).FirstOrDefault(); }
        }

        public bool Random
        {
            get { return player.settings.getMode("shuffle"); }
            set { player.settings.setMode("shuffle", value); }
        }

        public bool Repeat
        {
            get { return player.settings.getMode("loop"); }
            set { player.settings.setMode("loop", value); }
        }

        public SongPlayer(Song song)
        {
            player.MediaError += PlayerMediaError;
            player.currentPlaylist.clear();
            player.settings.autoStart = true;
            player.URL = song.Url;
            player.CurrentItemChange += new _WMPOCXEvents_CurrentItemChangeEventHandler(player_CurrentItemChange);
        }

        private void player_CurrentItemChange(object pdispmedia)
        {
            throw new NotImplementedException();
        }

        public SongPlayer(IList<Song> playlist)
        {
            player.MediaError += PlayerMediaError; 
            player.currentPlaylist.clear();
            CurrentPlaylist = playlist;
            foreach (Song song in playlist)
            {
                player.currentPlaylist.appendItem(player.newMedia(song.Url));
            }
            player.controls.play();
            
        }

        void PlayerMediaError(object pMediaObject)
        {
            throw new Exception(pMediaObject.ToString());
        }

        public int Volume
        {
            get { return player.settings.volume; }
            set { player.settings.volume = value; }
        }
        
        public double Position
        {
            get
            {
                return player == null ? 0 : player.controls.currentPosition;
            }
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

        public int CurrentSongDuration
        {
            get { return Convert.ToInt32(CurrentSong.Duration); }
        }

        public int CurrentSongIndex
        {
            get { return CurrentPlaylist.IndexOf(CurrentSong); }
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

        public void PlayItemAt(int index)
        {
            player.controls.stop();
            player.controls.currentItem = player.currentPlaylist.Item[index];
            player.controls.play();
        }

        void PlayerPlayStateChange(int newState)
        {
            if ((WMPPlayState)newState == WMPPlayState.wmppsStopped ||
                (WMPPlayState)newState == WMPPlayState.wmppsReady)
                State = PlaybackState.Stopped;
            if ((WMPPlayState)newState == WMPPlayState.wmppsPaused)
                State = PlaybackState.Paused;
            if ((WMPPlayState)newState == WMPPlayState.wmppsPlaying)
                State = PlaybackState.Stopped;
            if ((WMPPlayState)newState == WMPPlayState.wmppsBuffering)
                State = PlaybackState.Buffering;
            else State = PlaybackState.Unknown;
        }

        public void Stop()
        {
            player.controls.stop();
        }

        public void Pause()
        {
            if (State == PlaybackState.Playing)
                player.controls.pause();
        }

        public void Resume()
        {
            if (State == PlaybackState.Paused)
                player.controls.play();
        }


        public void Dispose()
        {
            try
            {
                player.controls.stop();
                player = null;
            }
            catch
            {
            }
        }

        public enum PlaybackState
        {
            Stopped, Playing, Paused, Buffering, Unknown
        }

        public void Skip()
        {
            player.controls.next();
        }
    }
}
