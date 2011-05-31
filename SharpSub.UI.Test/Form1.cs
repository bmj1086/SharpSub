using System;
using System.Configuration;
using System.Windows.Forms;
using SharpSub.Data;
using SharpSub.Data.SubsonicItemTypes;
using SubsonicAPI;
using System.Diagnostics;

namespace SharpSub.UI.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region login
            string url = ConfigurationManager.AppSettings["TestServerURL"];
            string username = ConfigurationManager.AppSettings["TestServerUsername"];
            string password = ConfigurationManager.AppSettings["TestServerPassword"];
            var loginOk = Subsonic.LogIn(url, username, password);
            #endregion

            if (loginOk)
            {
                MusicPlayer player = new MusicPlayer();
                var song = new Song("All Things", "533a5c4d757369635c4161726f6e2047696c6c65737069655c416e7468656d20536f6e67202832303131295c303120416c6c205468696e67732e6d3461");
                player.addToPlaylist(song);

                player.Play();
            }
            else
            {
                Debug.WriteLine("Didn't work");
            }
            
        }
    }
}
