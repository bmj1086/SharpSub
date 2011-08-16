namespace SuperSonicUI
{
    partial class MainForm
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
            this.DurationLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NowPlayingAlbumName = new System.Windows.Forms.Label();
            this.NowPlayingSongName = new System.Windows.Forms.Label();
            this.SkipForwardPictureBox = new System.Windows.Forms.PictureBox();
            this.PlayPausePictureBox = new System.Windows.Forms.PictureBox();
            this.SkipBackPictureBox = new System.Windows.Forms.PictureBox();
            this.volumeTrackBar = new SuperSonicTrackBarLib.SuperSonicTrackBar();
            this.CurrentPlayingAlbumArt = new System.Windows.Forms.PictureBox();
            this.CurrentPositionLabel = new System.Windows.Forms.Label();
            this.PositionTrackBar = new SuperSonicTrackBarLib.SuperSonicTrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.FormIconPictureBox = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SkipForwardPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayPausePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkipBackPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPlayingAlbumArt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormIconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DurationLabel
            // 
            this.DurationLabel.AutoEllipsis = true;
            this.DurationLabel.Location = new System.Drawing.Point(724, 412);
            this.DurationLabel.Name = "DurationLabel";
            this.DurationLabel.Size = new System.Drawing.Size(50, 23);
            this.DurationLabel.TabIndex = 8;
            this.DurationLabel.Text = "0:00";
            this.DurationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(983, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 23);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.SkipForwardPictureBox);
            this.splitContainer1.Panel1.Controls.Add(this.PlayPausePictureBox);
            this.splitContainer1.Panel1.Controls.Add(this.SkipBackPictureBox);
            this.splitContainer1.Panel1.Controls.Add(this.volumeTrackBar);
            this.splitContainer1.Panel1.Controls.Add(this.CurrentPlayingAlbumArt);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.CurrentPositionLabel);
            this.splitContainer1.Panel2.Controls.Add(this.PositionTrackBar);
            this.splitContainer1.Panel2.Controls.Add(this.DurationLabel);
            this.splitContainer1.Size = new System.Drawing.Size(984, 438);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.panel1.Controls.Add(this.NowPlayingAlbumName);
            this.panel1.Controls.Add(this.NowPlayingSongName);
            this.panel1.Location = new System.Drawing.Point(0, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 41);
            this.panel1.TabIndex = 12;
            // 
            // NowPlayingAlbumName
            // 
            this.NowPlayingAlbumName.AutoEllipsis = true;
            this.NowPlayingAlbumName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NowPlayingAlbumName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.NowPlayingAlbumName.Location = new System.Drawing.Point(14, 23);
            this.NowPlayingAlbumName.Name = "NowPlayingAlbumName";
            this.NowPlayingAlbumName.Size = new System.Drawing.Size(177, 13);
            this.NowPlayingAlbumName.TabIndex = 1;
            this.NowPlayingAlbumName.Text = "Now Playing";
            this.NowPlayingAlbumName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NowPlayingSongName
            // 
            this.NowPlayingSongName.AutoEllipsis = true;
            this.NowPlayingSongName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NowPlayingSongName.Location = new System.Drawing.Point(11, 2);
            this.NowPlayingSongName.Name = "NowPlayingSongName";
            this.NowPlayingSongName.Size = new System.Drawing.Size(180, 18);
            this.NowPlayingSongName.TabIndex = 0;
            this.NowPlayingSongName.Text = "Now Playing";
            this.NowPlayingSongName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SkipForwardPictureBox
            // 
            this.SkipForwardPictureBox.Image = global::SuperSonicUI.Properties.Resources.SkipForwardButton32;
            this.SkipForwardPictureBox.Location = new System.Drawing.Point(72, 409);
            this.SkipForwardPictureBox.Name = "SkipForwardPictureBox";
            this.SkipForwardPictureBox.Size = new System.Drawing.Size(25, 25);
            this.SkipForwardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SkipForwardPictureBox.TabIndex = 11;
            this.SkipForwardPictureBox.TabStop = false;
            // 
            // PlayPausePictureBox
            // 
            this.PlayPausePictureBox.Image = global::SuperSonicUI.Properties.Resources.PlayButton32;
            this.PlayPausePictureBox.Location = new System.Drawing.Point(34, 405);
            this.PlayPausePictureBox.Name = "PlayPausePictureBox";
            this.PlayPausePictureBox.Size = new System.Drawing.Size(32, 32);
            this.PlayPausePictureBox.TabIndex = 10;
            this.PlayPausePictureBox.TabStop = false;
            this.PlayPausePictureBox.Click += new System.EventHandler(this.PlayPausePictureBoxClick);
            // 
            // SkipBackPictureBox
            // 
            this.SkipBackPictureBox.Image = global::SuperSonicUI.Properties.Resources.SkipBackButton32;
            this.SkipBackPictureBox.Location = new System.Drawing.Point(3, 409);
            this.SkipBackPictureBox.Name = "SkipBackPictureBox";
            this.SkipBackPictureBox.Size = new System.Drawing.Size(25, 25);
            this.SkipBackPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SkipBackPictureBox.TabIndex = 9;
            this.SkipBackPictureBox.TabStop = false;
            // 
            // volumeTrackBar
            // 
            this.volumeTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.volumeTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.volumeTrackBar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.volumeTrackBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.volumeTrackBar.IndentHeight = 6;
            this.volumeTrackBar.Location = new System.Drawing.Point(101, 412);
            this.volumeTrackBar.Maximum = 10;
            this.volumeTrackBar.Minimum = 0;
            this.volumeTrackBar.Name = "volumeTrackBar";
            this.volumeTrackBar.Size = new System.Drawing.Size(97, 19);
            this.volumeTrackBar.TabIndex = 9;
            this.volumeTrackBar.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.volumeTrackBar.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.volumeTrackBar.TickHeight = 4;
            this.volumeTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.volumeTrackBar.TrackerColor = System.Drawing.Color.White;
            this.volumeTrackBar.TrackerSize = new System.Drawing.Size(7, 7);
            this.volumeTrackBar.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.volumeTrackBar.TrackLineHeight = 7;
            this.volumeTrackBar.Value = 0;
            // 
            // CurrentPlayingAlbumArt
            // 
            this.CurrentPlayingAlbumArt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentPlayingAlbumArt.Location = new System.Drawing.Point(1, 202);
            this.CurrentPlayingAlbumArt.Name = "CurrentPlayingAlbumArt";
            this.CurrentPlayingAlbumArt.Size = new System.Drawing.Size(201, 201);
            this.CurrentPlayingAlbumArt.TabIndex = 0;
            this.CurrentPlayingAlbumArt.TabStop = false;
            // 
            // CurrentPositionLabel
            // 
            this.CurrentPositionLabel.AutoEllipsis = true;
            this.CurrentPositionLabel.Location = new System.Drawing.Point(2, 413);
            this.CurrentPositionLabel.Name = "CurrentPositionLabel";
            this.CurrentPositionLabel.Size = new System.Drawing.Size(53, 23);
            this.CurrentPositionLabel.TabIndex = 11;
            this.CurrentPositionLabel.Text = "0:00";
            this.CurrentPositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PositionTrackBar
            // 
            this.PositionTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.PositionTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.PositionTrackBar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionTrackBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.PositionTrackBar.IndentHeight = 6;
            this.PositionTrackBar.Location = new System.Drawing.Point(61, 413);
            this.PositionTrackBar.Maximum = 10;
            this.PositionTrackBar.Minimum = 0;
            this.PositionTrackBar.Name = "PositionTrackBar";
            this.PositionTrackBar.Size = new System.Drawing.Size(657, 21);
            this.PositionTrackBar.TabIndex = 10;
            this.PositionTrackBar.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.PositionTrackBar.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.PositionTrackBar.TickHeight = 4;
            this.PositionTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.PositionTrackBar.TrackerColor = System.Drawing.Color.White;
            this.PositionTrackBar.TrackerSize = new System.Drawing.Size(9, 9);
            this.PositionTrackBar.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PositionTrackBar.TrackLineHeight = 9;
            this.PositionTrackBar.Value = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(928, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "SuperSonic";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormIconPictureBox
            // 
            this.FormIconPictureBox.Location = new System.Drawing.Point(2, 2);
            this.FormIconPictureBox.Name = "FormIconPictureBox";
            this.FormIconPictureBox.Size = new System.Drawing.Size(19, 19);
            this.FormIconPictureBox.TabIndex = 9;
            this.FormIconPictureBox.TabStop = false;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(961, 2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(19, 19);
            this.CloseButton.TabIndex = 12;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(983, 483);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.FormIconPictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SkipForwardPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayPausePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SkipBackPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPlayingAlbumArt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FormIconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CurrentPlayingAlbumArt;
        private System.Windows.Forms.Label DurationLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox FormIconPictureBox;
        private SuperSonicTrackBarLib.SuperSonicTrackBar volumeTrackBar;
        private System.Windows.Forms.PictureBox SkipForwardPictureBox;
        private System.Windows.Forms.PictureBox PlayPausePictureBox;
        private System.Windows.Forms.PictureBox SkipBackPictureBox;
        private System.Windows.Forms.Label CurrentPositionLabel;
        private SuperSonicTrackBarLib.SuperSonicTrackBar PositionTrackBar;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label NowPlayingAlbumName;
        private System.Windows.Forms.Label NowPlayingSongName;
    }
}

