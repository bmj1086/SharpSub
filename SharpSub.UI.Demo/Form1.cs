using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpSub.Data;

namespace SharpSub.UI.Demo
{
    public partial class Form1 : Form
    {
        private IList<Artist> _artists { get; set; }

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
            var artist = (Artist) artistListBox.SelectedItem;
            albumsListBox.DataSource = SubsonicRequest.GetArtistAlbums(artist.ID);
        }

        private void albumsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var album = (Album) albumsListBox.SelectedItem;
            var songsList = SubsonicRequest.GetAlbumSongs(album.ID);
            songsDataGridView.DataSource = songsList;
        }
    }
}
