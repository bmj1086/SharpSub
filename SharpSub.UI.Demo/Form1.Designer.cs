namespace SharpSub.UI.Demo
{
    partial class Form1
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
            this.artistListBox = new System.Windows.Forms.ListBox();
            this.albumsListBox = new System.Windows.Forms.ListBox();
            this.songsDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.songsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // artistListBox
            // 
            this.artistListBox.FormattingEnabled = true;
            this.artistListBox.Location = new System.Drawing.Point(12, 12);
            this.artistListBox.Name = "artistListBox";
            this.artistListBox.Size = new System.Drawing.Size(189, 199);
            this.artistListBox.TabIndex = 0;
            this.artistListBox.SelectedIndexChanged += new System.EventHandler(this.artistListBox_SelectedIndexChanged);
            // 
            // albumsListBox
            // 
            this.albumsListBox.FormattingEnabled = true;
            this.albumsListBox.Location = new System.Drawing.Point(208, 13);
            this.albumsListBox.Name = "albumsListBox";
            this.albumsListBox.Size = new System.Drawing.Size(185, 199);
            this.albumsListBox.TabIndex = 1;
            this.albumsListBox.SelectedIndexChanged += new System.EventHandler(this.albumsListBox_SelectedIndexChanged);
            // 
            // songsDataGridView
            // 
            this.songsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.songsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.songsDataGridView.Location = new System.Drawing.Point(12, 217);
            this.songsDataGridView.Name = "songsDataGridView";
            this.songsDataGridView.Size = new System.Drawing.Size(381, 209);
            this.songsDataGridView.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 438);
            this.Controls.Add(this.songsDataGridView);
            this.Controls.Add(this.albumsListBox);
            this.Controls.Add(this.artistListBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.songsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox artistListBox;
        private System.Windows.Forms.ListBox albumsListBox;
        private System.Windows.Forms.DataGridView songsDataGridView;
    }
}

