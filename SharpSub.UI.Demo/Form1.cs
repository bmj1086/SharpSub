using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SharpSub.Data;

namespace SharpSub.UI.Demo
{
    public partial class Form1 : Form
    {
        private Queue<Delegate> todo = new Queue<Delegate>();
        public Form1()
        {
            InitializeComponent();
            string serverURL = "bmjones.com:56565/music";
            string username = "Guest";
            string password = "notbrett";
            SubsonicResponse loginResponse = SubsonicRequest.Login(serverURL, username, password);

            if (!loginResponse.Successful)
                return;

            artistListBox.DataSource = SubsonicRequest.GetArtistList();
        }

        private void artistListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var artist = (Artist)artistListBox.SelectedItem;
            ThreadPool.QueueUserWorkItem((ShowAlbums), artist);
        }

        private void albumsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var album = (Album)albumsListBox.SelectedItem;
            ThreadPool.QueueUserWorkItem((ShowSongs), album);
        }

        private void ShowSongs(object album)
        {
            IList<Song> songsList = SubsonicRequest.GetAlbumSongs((album as Album).ID);
            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { songsDataGridView.DataSource = songsList; }));
        }

        private void ShowAlbums(object artist)
        {
            IList<Album> albumList = SubsonicRequest.GetArtistAlbums((artist as Artist).ID);
            if (InvokeRequired)
                Invoke(new MethodInvoker(delegate { albumsListBox.DataSource = albumList; }));
        }

        private void songsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var songList = (IList<Song>)songsDataGridView.DataSource;
            Song song = songList.ElementAt(songsDataGridView.SelectedCells[0].RowIndex);
            //TODO: Play song
        }


    }
}
