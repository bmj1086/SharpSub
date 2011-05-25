namespace SharpSub
{
    partial class SetupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.histGroupBox = new System.Windows.Forms.GroupBox();
            this.histAutoConnCheckbox = new System.Windows.Forms.CheckBox();
            this.histConnectButton = new System.Windows.Forms.Button();
            this.histComboBox = new System.Windows.Forms.ComboBox();
            this.newGroupBox = new System.Windows.Forms.GroupBox();
            this.newTestConnButton = new System.Windows.Forms.Button();
            this.newAutoConnCheckbox = new System.Windows.Forms.CheckBox();
            this.newConnectButton = new System.Windows.Forms.Button();
            this.newPasswordTextbox = new System.Windows.Forms.TextBox();
            this.newUsernameTextbox = new System.Windows.Forms.TextBox();
            this.newUrlTextbox = new System.Windows.Forms.TextBox();
            this.newConnectionNameTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.histGroupBox.SuspendLayout();
            this.newGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // histGroupBox
            // 
            this.histGroupBox.Controls.Add(this.histAutoConnCheckbox);
            this.histGroupBox.Controls.Add(this.histConnectButton);
            this.histGroupBox.Controls.Add(this.histComboBox);
            this.histGroupBox.Location = new System.Drawing.Point(32, 29);
            this.histGroupBox.Name = "histGroupBox";
            this.histGroupBox.Size = new System.Drawing.Size(386, 83);
            this.histGroupBox.TabIndex = 0;
            this.histGroupBox.TabStop = false;
            this.histGroupBox.Text = "Select a Previously Saved Server";
            // 
            // histAutoConnCheckbox
            // 
            this.histAutoConnCheckbox.AutoSize = true;
            this.histAutoConnCheckbox.Location = new System.Drawing.Point(24, 58);
            this.histAutoConnCheckbox.Name = "histAutoConnCheckbox";
            this.histAutoConnCheckbox.Size = new System.Drawing.Size(130, 17);
            this.histAutoConnCheckbox.TabIndex = 2;
            this.histAutoConnCheckbox.Text = "Automatically connect";
            this.histAutoConnCheckbox.UseVisualStyleBackColor = true;
            // 
            // histConnectButton
            // 
            this.histConnectButton.Enabled = false;
            this.histConnectButton.Location = new System.Drawing.Point(296, 30);
            this.histConnectButton.Name = "histConnectButton";
            this.histConnectButton.Size = new System.Drawing.Size(75, 23);
            this.histConnectButton.TabIndex = 1;
            this.histConnectButton.Text = "Connect";
            this.histConnectButton.UseVisualStyleBackColor = true;
            // 
            // histComboBox
            // 
            this.histComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.histComboBox.FormattingEnabled = true;
            this.histComboBox.Location = new System.Drawing.Point(24, 30);
            this.histComboBox.Name = "histComboBox";
            this.histComboBox.Size = new System.Drawing.Size(252, 21);
            this.histComboBox.TabIndex = 0;
            // 
            // newGroupBox
            // 
            this.newGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newGroupBox.Controls.Add(this.newTestConnButton);
            this.newGroupBox.Controls.Add(this.newAutoConnCheckbox);
            this.newGroupBox.Controls.Add(this.newConnectButton);
            this.newGroupBox.Controls.Add(this.newPasswordTextbox);
            this.newGroupBox.Controls.Add(this.newUsernameTextbox);
            this.newGroupBox.Controls.Add(this.newUrlTextbox);
            this.newGroupBox.Controls.Add(this.newConnectionNameTextbox);
            this.newGroupBox.Controls.Add(this.label4);
            this.newGroupBox.Controls.Add(this.label3);
            this.newGroupBox.Controls.Add(this.label2);
            this.newGroupBox.Controls.Add(this.label1);
            this.newGroupBox.Location = new System.Drawing.Point(32, 154);
            this.newGroupBox.Name = "newGroupBox";
            this.newGroupBox.Size = new System.Drawing.Size(386, 190);
            this.newGroupBox.TabIndex = 1;
            this.newGroupBox.TabStop = false;
            this.newGroupBox.Text = "Create a new server connection";
            // 
            // newTestConnButton
            // 
            this.newTestConnButton.Enabled = false;
            this.newTestConnButton.Location = new System.Drawing.Point(170, 147);
            this.newTestConnButton.Name = "newTestConnButton";
            this.newTestConnButton.Size = new System.Drawing.Size(118, 23);
            this.newTestConnButton.TabIndex = 9;
            this.newTestConnButton.Text = "Test Connection";
            this.newTestConnButton.UseVisualStyleBackColor = true;
            this.newTestConnButton.Click += new System.EventHandler(this.newTestConnButton_Click);
            // 
            // newAutoConnCheckbox
            // 
            this.newAutoConnCheckbox.AutoSize = true;
            this.newAutoConnCheckbox.Location = new System.Drawing.Point(13, 151);
            this.newAutoConnCheckbox.Name = "newAutoConnCheckbox";
            this.newAutoConnCheckbox.Size = new System.Drawing.Size(130, 17);
            this.newAutoConnCheckbox.TabIndex = 8;
            this.newAutoConnCheckbox.Text = "Automatically connect";
            this.newAutoConnCheckbox.UseVisualStyleBackColor = true;
            // 
            // newConnectButton
            // 
            this.newConnectButton.Enabled = false;
            this.newConnectButton.Location = new System.Drawing.Point(296, 147);
            this.newConnectButton.Name = "newConnectButton";
            this.newConnectButton.Size = new System.Drawing.Size(75, 23);
            this.newConnectButton.TabIndex = 3;
            this.newConnectButton.Text = "Connect";
            this.newConnectButton.UseVisualStyleBackColor = true;
            // 
            // newPasswordTextbox
            // 
            this.newPasswordTextbox.Location = new System.Drawing.Point(221, 110);
            this.newPasswordTextbox.Name = "newPasswordTextbox";
            this.newPasswordTextbox.PasswordChar = '*';
            this.newPasswordTextbox.Size = new System.Drawing.Size(150, 20);
            this.newPasswordTextbox.TabIndex = 7;
            // 
            // newUsernameTextbox
            // 
            this.newUsernameTextbox.Location = new System.Drawing.Point(24, 110);
            this.newUsernameTextbox.Name = "newUsernameTextbox";
            this.newUsernameTextbox.Size = new System.Drawing.Size(150, 20);
            this.newUsernameTextbox.TabIndex = 6;
            // 
            // newUrlTextbox
            // 
            this.newUrlTextbox.Location = new System.Drawing.Point(221, 52);
            this.newUrlTextbox.Name = "newUrlTextbox";
            this.newUrlTextbox.Size = new System.Drawing.Size(150, 20);
            this.newUrlTextbox.TabIndex = 5;
            // 
            // newConnectionNameTextbox
            // 
            this.newConnectionNameTextbox.Location = new System.Drawing.Point(24, 52);
            this.newConnectionNameTextbox.Name = "newConnectionNameTextbox";
            this.newConnectionNameTextbox.Size = new System.Drawing.Size(150, 20);
            this.newConnectionNameTextbox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Connection Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server URL:";
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 385);
            this.Controls.Add(this.newGroupBox);
            this.Controls.Add(this.histGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetupForm";
            this.Text = "Setup Your Account";
            this.histGroupBox.ResumeLayout(false);
            this.histGroupBox.PerformLayout();
            this.newGroupBox.ResumeLayout(false);
            this.newGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox histGroupBox;
        private System.Windows.Forms.CheckBox histAutoConnCheckbox;
        private System.Windows.Forms.Button histConnectButton;
        private System.Windows.Forms.ComboBox histComboBox;
        private System.Windows.Forms.GroupBox newGroupBox;
        private System.Windows.Forms.CheckBox newAutoConnCheckbox;
        private System.Windows.Forms.Button newConnectButton;
        private System.Windows.Forms.TextBox newPasswordTextbox;
        private System.Windows.Forms.TextBox newUsernameTextbox;
        private System.Windows.Forms.TextBox newUrlTextbox;
        private System.Windows.Forms.TextBox newConnectionNameTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newTestConnButton;
    }
}

