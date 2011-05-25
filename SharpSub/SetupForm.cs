using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SharpSub
{
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        private void newTestConnButton_Click(object sender, EventArgs e)
        {
            string url = newUrlTextbox.Text.Trim();
            string name = newConnectionNameTextbox.Text.Trim();
            string username = newUsernameTextbox.Text.Trim();
            string password = newPasswordTextbox.Text.Trim();

            var testOkay = Server.TestConnection(url: url, username: username, password: password);

            if (!testOkay)
            {
                //TODO: tell UI that the server didn't connect
                Debug.WriteLine("Test connection failed");
            }    


        }
    }
}
