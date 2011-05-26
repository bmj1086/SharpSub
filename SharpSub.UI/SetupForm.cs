using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SharpSub.Data;

namespace SharpSub
{
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
#if DEBUG
            AddTestServerCredentials();
#endif
        }

        private void AddTestServerCredentials()
        {
            newUrlTextbox.Text = ConfigurationManager.AppSettings["TestServerURL"];
            newConnectionNameTextbox.Text = ConfigurationManager.AppSettings["TestServerName"];
            newUsernameTextbox.Text = ConfigurationManager.AppSettings["TestServerUsername"];
            newPasswordTextbox.Text = ConfigurationManager.AppSettings["TestServerPassword"];
        }

        private void NewTestConnButtonClick(object sender, EventArgs e)
        {
            string url = newUrlTextbox.Text.Trim();
            string name = newConnectionNameTextbox.Text.Trim();
            string username = newUsernameTextbox.Text.Trim();
            string password = newPasswordTextbox.Text.Trim();

            var testOkay = Server.TestConnection(url: url, username: username, password: password);

            if (!testOkay)
            {
               //TODO: tell UI that the server didn't connect
                MessageBox.Show("Server did not connect");
                Debug.WriteLine("Test connection failed");
            }    


        }
    }
}
