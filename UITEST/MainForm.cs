/* Dev Notes:
 * Now Playing Box above album art should be gradiant b4b4b4 (top) to 919191 (bottom)
 * 
 */

using System;
using System.Linq;
using System.Windows.Forms;
using SharpSub.Data;
using SharpSub.LastFm;
using SuperSonicUI.Properties;
using WMPLib;

namespace SuperSonicUI
{
    public partial class MainForm : Form, IDisposable
    {
        private readonly LastFm lastFm = new LastFm("15790800a7aebdfd99d3328353b09f07", "70573d54a452a512c1fbae5fdfaeddb4");
        private ArtistInfo currentArtistInfo;
        private SongPlayer player;
        private Song CurrentSong { get; set; }
        private Timer timer;
        private bool disposed;

        public MainForm()
        {
            InitializeComponent();
            if (!SubsonicRequest.Login("bmjones.com/music", "Guest", "notbrett").Successful)
            {
                MessageBox.Show("Login failed");
                return;
            }
            CurrentSong = SubsonicRequest.GetRandomSongs(1).FirstOrDefault();
            player = new SongPlayer(CurrentSong);
            AssignPlayerEvents();
            CreateTimer();
            SetCurrentSongInfo();
        }

        private void AssignPlayerEvents()
        {
            player.CurrentItemChanged += PlayerCurrentItemChanged;
            player.PlaybackStateChanged += PlayerPlaybackStateChanged;
            player.MediaError += PlayerMediaError;
        }

        private void PlayerMediaError(object pmediaobject)
        {
            throw new Exception("Player Media Error...");
        }

        void PlayerPlaybackStateChanged(int newState)
        {
            toolStripStatusLabel.Text = player.PlaybackState.ToString().Replace("wmpps", String.Empty);

            switch (newState)
            {
                case (int)WMPPlayState.wmppsStopped:
                case (int)WMPPlayState.wmppsPaused:
                    PlayPausePictureBox.Image = Resources.PlayButton32;
                    timer.Stop();
                    break;
                case (int)WMPPlayState.wmppsPlaying:
                    PlayPausePictureBox.Image = Resources.PauseButton32;
                    timer.Start();
                    break;
            }

        }

        void PlayerCurrentItemChanged(object pdispMedia)
        {
            CurrentSong = player.CurrentSong;
            SetCurrentSongInfo();
            timer.Start();
        }

        private void CreateTimer()
        {
            timer = new Timer { Interval = 100, Enabled = true };
            timer.Tick += TimerTick;
        }

        void TimerTick(object sender, EventArgs e)
        {
            try
            {
                PositionTrackBar.Value = Convert.ToInt32(player.Position);
                DurationLabel.Text = player.DurationString();
                CurrentPositionLabel.Text = player.PositionString();
            }
            catch
            {
                timer.Stop();
                MessageBox.Show("Test");
            }
            
        }

        private void SetCurrentSongInfo()
        {
            CurrentPlayingAlbumArt.Image = CurrentSong.CoverArt(CurrentPlayingAlbumArt.Width);
            PositionTrackBar.Maximum = (int) CurrentSong.Duration;
            DurationLabel.Text = player.DurationString();
            NowPlayingSongName.Text = CurrentSong.Title;
            NowPlayingAlbumName.Text = CurrentSong.Album;

            try // Last fm stuff
            {
                Artist artist = SubsonicRequest.GetArtistList().
                   Where(a => a.ID == CurrentSong.ID).
                   FirstOrDefault();

                currentArtistInfo = lastFm.GetArtistInfo(artist);
                currentArtistInfo.Image(ArtistImageSize.Large);

                if (currentArtistInfo == null) return;

            }
            catch { }
            
        }


        #region IDisposable members
        ~MainForm()
        {
            DisposeObject(false);
        }

        public void Dispose()
        {
            //player.Dispose();
            DisposeObject(true);
            GC.SuppressFinalize(this);
        }

        private void DisposeObject(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // Dispose managed resources.
            }
            // Dispose unmanaged resources.
            disposed = true;
        }
        #endregion

        private void MainFormLoad(object sender, EventArgs e)
        {
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PlayPausePictureBoxClick(object sender, EventArgs e)
        {
            switch ((int) player.PlaybackState)
            {
                case (int)WMPPlayState.wmppsPaused:
                    player.Resume();
                    timer.Start();
                    break;
                case (int)WMPPlayState.wmppsPlaying:
                    player.Pause();
                    timer.Stop();
                    break;
            }
        }
    }
}
