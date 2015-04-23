namespace ACLencoder
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
            this.components = new System.ComponentModel.Container();
            this.TargetGroupBox = new System.Windows.Forms.GroupBox();
            this.TargetLabel = new System.Windows.Forms.Label();
            this.ChooseEncodePathButton = new System.Windows.Forms.Button();
            this.TargetTextBox = new System.Windows.Forms.TextBox();
            this.EncodeButton = new System.Windows.Forms.Button();
            this.RemoveEncodedFileButton = new System.Windows.Forms.Button();
            this.DecodeButton = new System.Windows.Forms.Button();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.EncodeOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DecodeSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FileListGroupBox = new System.Windows.Forms.GroupBox();
            this.FileListLabel = new System.Windows.Forms.Label();
            this.CreateFileListButton = new System.Windows.Forms.Button();
            this.ChooseFileListButton = new System.Windows.Forms.Button();
            this.FileListTextBox = new System.Windows.Forms.TextBox();
            this.StatusOverTimer = new System.Windows.Forms.Timer(this.components);
            this.TargetGroupBox.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.FileListGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TargetGroupBox
            // 
            this.TargetGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetGroupBox.Controls.Add(this.TargetLabel);
            this.TargetGroupBox.Controls.Add(this.ChooseEncodePathButton);
            this.TargetGroupBox.Controls.Add(this.TargetTextBox);
            this.TargetGroupBox.Location = new System.Drawing.Point(4, 4);
            this.TargetGroupBox.Name = "TargetGroupBox";
            this.TargetGroupBox.Size = new System.Drawing.Size(472, 67);
            this.TargetGroupBox.TabIndex = 0;
            this.TargetGroupBox.TabStop = false;
            this.TargetGroupBox.Text = "Target:";
            // 
            // TargetLabel
            // 
            this.TargetLabel.AutoSize = true;
            this.TargetLabel.Location = new System.Drawing.Point(42, 45);
            this.TargetLabel.Name = "TargetLabel";
            this.TargetLabel.Size = new System.Drawing.Size(368, 13);
            this.TargetLabel.TabIndex = 3;
            this.TargetLabel.Text = "This is the file you want to encode to (or decode from) the files listed below";
            // 
            // ChooseEncodePathButton
            // 
            this.ChooseEncodePathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseEncodePathButton.Location = new System.Drawing.Point(436, 19);
            this.ChooseEncodePathButton.Name = "ChooseEncodePathButton";
            this.ChooseEncodePathButton.Size = new System.Drawing.Size(30, 23);
            this.ChooseEncodePathButton.TabIndex = 2;
            this.ChooseEncodePathButton.Text = "...";
            this.ChooseEncodePathButton.UseVisualStyleBackColor = true;
            this.ChooseEncodePathButton.Click += new System.EventHandler(this.ChooseTargetPathButton_Click);
            // 
            // TargetTextBox
            // 
            this.TargetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetTextBox.Location = new System.Drawing.Point(7, 21);
            this.TargetTextBox.Name = "TargetTextBox";
            this.TargetTextBox.Size = new System.Drawing.Size(423, 21);
            this.TargetTextBox.TabIndex = 1;
            // 
            // EncodeButton
            // 
            this.EncodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EncodeButton.Location = new System.Drawing.Point(250, 68);
            this.EncodeButton.Name = "EncodeButton";
            this.EncodeButton.Size = new System.Drawing.Size(73, 52);
            this.EncodeButton.TabIndex = 8;
            this.EncodeButton.Text = "Encode";
            this.EncodeButton.UseVisualStyleBackColor = true;
            this.EncodeButton.Click += new System.EventHandler(this.EncodeButton_Click);
            // 
            // RemoveEncodedFileButton
            // 
            this.RemoveEncodedFileButton.Location = new System.Drawing.Point(137, 68);
            this.RemoveEncodedFileButton.Name = "RemoveEncodedFileButton";
            this.RemoveEncodedFileButton.Size = new System.Drawing.Size(81, 52);
            this.RemoveEncodedFileButton.TabIndex = 7;
            this.RemoveEncodedFileButton.Text = "Wipe ACLEncoded Entries";
            this.RemoveEncodedFileButton.UseVisualStyleBackColor = true;
            this.RemoveEncodedFileButton.Click += new System.EventHandler(this.RemoveEncodedFileButton_Click);
            // 
            // DecodeButton
            // 
            this.DecodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DecodeButton.Location = new System.Drawing.Point(363, 68);
            this.DecodeButton.Name = "DecodeButton";
            this.DecodeButton.Size = new System.Drawing.Size(67, 52);
            this.DecodeButton.TabIndex = 9;
            this.DecodeButton.Text = "Decode";
            this.DecodeButton.UseVisualStyleBackColor = true;
            this.DecodeButton.Click += new System.EventHandler(this.DecodeButton_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusToolStripStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 204);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(482, 22);
            this.MainStatusStrip.TabIndex = 1;
            // 
            // StatusToolStripStatusLabel
            // 
            this.StatusToolStripStatusLabel.Name = "StatusToolStripStatusLabel";
            this.StatusToolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.StatusToolStripStatusLabel.Text = "<idle>";
            // 
            // FileListGroupBox
            // 
            this.FileListGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FileListGroupBox.Controls.Add(this.FileListLabel);
            this.FileListGroupBox.Controls.Add(this.CreateFileListButton);
            this.FileListGroupBox.Controls.Add(this.EncodeButton);
            this.FileListGroupBox.Controls.Add(this.ChooseFileListButton);
            this.FileListGroupBox.Controls.Add(this.FileListTextBox);
            this.FileListGroupBox.Controls.Add(this.RemoveEncodedFileButton);
            this.FileListGroupBox.Controls.Add(this.DecodeButton);
            this.FileListGroupBox.Location = new System.Drawing.Point(4, 75);
            this.FileListGroupBox.Name = "FileListGroupBox";
            this.FileListGroupBox.Size = new System.Drawing.Size(472, 126);
            this.FileListGroupBox.TabIndex = 3;
            this.FileListGroupBox.TabStop = false;
            this.FileListGroupBox.Text = "File List:";
            // 
            // FileListLabel
            // 
            this.FileListLabel.AutoSize = true;
            this.FileListLabel.Location = new System.Drawing.Point(12, 45);
            this.FileListLabel.Name = "FileListLabel";
            this.FileListLabel.Size = new System.Drawing.Size(418, 13);
            this.FileListLabel.TabIndex = 10;
            this.FileListLabel.Text = "This is a TXT file that contains a list of file paths. Your target file will be e" +
                "ncoded here.";
            // 
            // CreateFileListButton
            // 
            this.CreateFileListButton.Location = new System.Drawing.Point(7, 68);
            this.CreateFileListButton.Name = "CreateFileListButton";
            this.CreateFileListButton.Size = new System.Drawing.Size(96, 52);
            this.CreateFileListButton.TabIndex = 6;
            this.CreateFileListButton.Text = "Create FileList";
            this.CreateFileListButton.UseVisualStyleBackColor = true;
            this.CreateFileListButton.Click += new System.EventHandler(this.CreateFileListButton_Click);
            // 
            // ChooseFileListButton
            // 
            this.ChooseFileListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseFileListButton.Location = new System.Drawing.Point(436, 19);
            this.ChooseFileListButton.Name = "ChooseFileListButton";
            this.ChooseFileListButton.Size = new System.Drawing.Size(30, 23);
            this.ChooseFileListButton.TabIndex = 5;
            this.ChooseFileListButton.Text = "...";
            this.ChooseFileListButton.UseVisualStyleBackColor = true;
            this.ChooseFileListButton.Click += new System.EventHandler(this.ChooseFileListButton_Click);
            // 
            // FileListTextBox
            // 
            this.FileListTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FileListTextBox.Location = new System.Drawing.Point(7, 21);
            this.FileListTextBox.Name = "FileListTextBox";
            this.FileListTextBox.Size = new System.Drawing.Size(423, 21);
            this.FileListTextBox.TabIndex = 4;
            // 
            // StatusOverTimer
            // 
            this.StatusOverTimer.Interval = 3000;
            this.StatusOverTimer.Tick += new System.EventHandler(this.StatusOverTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 226);
            this.Controls.Add(this.FileListGroupBox);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.TargetGroupBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "ACLEncode";
            this.TargetGroupBox.ResumeLayout(false);
            this.TargetGroupBox.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.FileListGroupBox.ResumeLayout(false);
            this.FileListGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox TargetGroupBox;
        private System.Windows.Forms.Button ChooseEncodePathButton;
        private System.Windows.Forms.TextBox TargetTextBox;
        private System.Windows.Forms.Button EncodeButton;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.OpenFileDialog EncodeOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog DecodeSaveFileDialog;
        private System.Windows.Forms.GroupBox FileListGroupBox;
        private System.Windows.Forms.Button ChooseFileListButton;
        private System.Windows.Forms.TextBox FileListTextBox;
        private System.Windows.Forms.Button DecodeButton;
        private System.Windows.Forms.Button RemoveEncodedFileButton;
        private System.Windows.Forms.ToolStripStatusLabel StatusToolStripStatusLabel;
        private System.Windows.Forms.Timer StatusOverTimer;
        private System.Windows.Forms.Button CreateFileListButton;
        private System.Windows.Forms.Label TargetLabel;
        private System.Windows.Forms.Label FileListLabel;
    }
}

