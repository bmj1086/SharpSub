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
        public IList<Song> CurrentPlaylist;
        private WindowsMediaPlayer player = new WindowsMediaPlayer();

        public SongPlayer(Song song)
        {
            StartPlayer(new List<Song>{ song });
        }

        public SongPlayer(IList<Song> playlist)
        {
            StartPlayer(playlist);
        }

        private void StartPlayer(IList<Song> playlist)
        {
            MediaError += SongPlayerMediaError;
            player.MediaError += PlayerMediaError;
            player.currentPlaylist.clear();
            CurrentPlaylist = playlist;
            foreach (Song song in playlist)
            {
                player.currentPlaylist.appendItem(player.newMedia(song.Url));
            }
            player.controls.play();
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

        public int Volume
        {
            get { return player.settings.volume; }
            set { player.settings.volume = value; }
        }

        public double Position
        {
            get { return player == null ? 0 : player.controls.currentPosition; }
            set { player.controls.currentPosition = value; }
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

        public WMPPlayState PlaybackState
        {
            get { return player.playState; }
        }

        public string PlaybackStateString
        {
            get { return PlaybackState.ToString().Replace("wmpps", String.Empty); }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                player.controls.stop();
            }
            finally
            {
                player = null;
            }
        }

        #endregion

        public event _WMPOCXEvents_MediaErrorEventHandler MediaError
        {
            add { player.MediaError += value; }
            remove { player.MediaError -= value; }
        }
        public event _WMPOCXEvents_CurrentItemChangeEventHandler CurrentItemChanged
        {
            add { player.CurrentItemChange += value; }
            remove { player.CurrentItemChange -= value; }
        }

        public event _WMPOCXEvents_PlayStateChangeEventHandler PlaybackStateChanged
        {
            add { player.PlayStateChange += value; }
            remove { player.PlayStateChange -= value; }
        }

        void SongPlayerMediaError(object pMediaObject)
        {
            try
            {
                player.controls.play();
            }
            catch (Exception)
            {
            }
        }

        void PlayerMediaError(object pMediaObject)
        {
            throw new Exception(pMediaObject.ToString());
        }

        public string PositionString(string format = "mm:ss")
        {
            return ToTimeFormat((int)Position, format);
        }

        public string DurationString(string format = "mm:ss")
        {
            return ToTimeFormat((int)CurrentSong.Duration, format);
        }

        private string ToTimeFormat(int seconds, string format)
        {
            DateTime dt = DateTime.MinValue.AddSeconds(seconds);
            return dt.ToString("m:ss");
        }

        public void PlayItemAt(int index)
        {
            player.controls.currentItem = player.currentPlaylist.Item[index];
            player.controls.play();
        }

        public void Stop()
        {
            player.controls.stop();
        }

        public void Pause()
        {
            player.controls.pause();
        }

        public void Resume()
        {
            player.controls.play();
        }


        public void Skip()
        {
            player.controls.next();
        }
    }
}
