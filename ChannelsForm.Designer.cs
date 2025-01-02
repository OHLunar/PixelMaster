namespace PixelMaster
{
    partial class ChannelsForm
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
            pictureboxOriginal = new PictureBox();
            pictureboxTransformed = new PictureBox();
            trackbarRed = new TrackBar();
            trackbarBlue = new TrackBar();
            trackbarGreen = new TrackBar();
            btnPreview = new Button();
            lblRed = new Label();
            lblGreen = new Label();
            lblBlue = new Label();
            tbRed = new TextBox();
            tbBlue = new TextBox();
            tbGreen = new TextBox();
            btnCancel = new Button();
            btnApply = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureboxOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureboxTransformed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackbarRed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackbarBlue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackbarGreen).BeginInit();
            SuspendLayout();
            // 
            // pictureboxOriginal
            // 
            pictureboxOriginal.BackgroundImageLayout = ImageLayout.None;
            pictureboxOriginal.Location = new Point(115, 117);
            pictureboxOriginal.Name = "pictureboxOriginal";
            pictureboxOriginal.Size = new Size(408, 518);
            pictureboxOriginal.TabIndex = 0;
            pictureboxOriginal.TabStop = false;
            // 
            // pictureboxTransformed
            // 
            pictureboxTransformed.BackgroundImageLayout = ImageLayout.None;
            pictureboxTransformed.Location = new Point(700, 117);
            pictureboxTransformed.Name = "pictureboxTransformed";
            pictureboxTransformed.Size = new Size(408, 518);
            pictureboxTransformed.TabIndex = 1;
            pictureboxTransformed.TabStop = false;
            // 
            // trackbarRed
            // 
            trackbarRed.Location = new Point(170, 666);
            trackbarRed.Maximum = 100;
            trackbarRed.Name = "trackbarRed";
            trackbarRed.Size = new Size(300, 69);
            trackbarRed.TabIndex = 6;
            trackbarRed.Value = 100;
            trackbarRed.Scroll += trackbarRed_Scroll;
            // 
            // trackbarBlue
            // 
            trackbarBlue.Location = new Point(170, 741);
            trackbarBlue.Maximum = 100;
            trackbarBlue.Name = "trackbarBlue";
            trackbarBlue.Size = new Size(300, 69);
            trackbarBlue.TabIndex = 7;
            trackbarBlue.Value = 100;
            trackbarBlue.Scroll += trackbarBlue_Scroll;
            // 
            // trackbarGreen
            // 
            trackbarGreen.Location = new Point(170, 816);
            trackbarGreen.Maximum = 100;
            trackbarGreen.Name = "trackbarGreen";
            trackbarGreen.Size = new Size(300, 69);
            trackbarGreen.TabIndex = 8;
            trackbarGreen.Value = 100;
            trackbarGreen.Scroll += trackbarGreen_Scroll;
            // 
            // btnPreview
            // 
            btnPreview.Location = new Point(839, 722);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(140, 50);
            btnPreview.TabIndex = 9;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // lblRed
            // 
            lblRed.AutoSize = true;
            lblRed.Location = new Point(115, 666);
            lblRed.Name = "lblRed";
            lblRed.Size = new Size(42, 25);
            lblRed.TabIndex = 10;
            lblRed.Text = "Red";
            // 
            // lblGreen
            // 
            lblGreen.AutoSize = true;
            lblGreen.Location = new Point(115, 816);
            lblGreen.Name = "lblGreen";
            lblGreen.Size = new Size(58, 25);
            lblGreen.TabIndex = 11;
            lblGreen.Text = "Green";
            // 
            // lblBlue
            // 
            lblBlue.AutoSize = true;
            lblBlue.Location = new Point(115, 741);
            lblBlue.Name = "lblBlue";
            lblBlue.Size = new Size(45, 25);
            lblBlue.TabIndex = 12;
            lblBlue.Text = "Blue";
            // 
            // tbRed
            // 
            tbRed.Location = new Point(476, 666);
            tbRed.Name = "tbRed";
            tbRed.Size = new Size(47, 31);
            tbRed.TabIndex = 13;
            tbRed.TextChanged += tbRed_TextChanged;
            // 
            // tbBlue
            // 
            tbBlue.Location = new Point(476, 741);
            tbBlue.Name = "tbBlue";
            tbBlue.Size = new Size(47, 31);
            tbBlue.TabIndex = 14;
            tbBlue.TextChanged += tbBlue_TextChanged;
            // 
            // tbGreen
            // 
            tbGreen.Location = new Point(476, 816);
            tbGreen.Name = "tbGreen";
            tbGreen.Size = new Size(47, 31);
            tbGreen.TabIndex = 15;
            tbGreen.TextChanged += tbGreen_TextChanged;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(987, 999);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 50);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(1133, 999);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(140, 50);
            btnApply.TabIndex = 17;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // ChannelsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1285, 1061);
            Controls.Add(btnApply);
            Controls.Add(btnCancel);
            Controls.Add(tbGreen);
            Controls.Add(tbBlue);
            Controls.Add(tbRed);
            Controls.Add(lblBlue);
            Controls.Add(lblGreen);
            Controls.Add(lblRed);
            Controls.Add(btnPreview);
            Controls.Add(trackbarGreen);
            Controls.Add(trackbarBlue);
            Controls.Add(trackbarRed);
            Controls.Add(pictureboxTransformed);
            Controls.Add(pictureboxOriginal);
            Name = "ChannelsForm";
            Text = "Channels Mixer";
            ((System.ComponentModel.ISupportInitialize)pictureboxOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureboxTransformed).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackbarRed).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackbarBlue).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackbarGreen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureboxOriginal;
        private PictureBox pictureboxTransformed;
        private TrackBar trackbarRed;
        private TrackBar trackbarBlue;
        private TrackBar trackbarGreen;
        private Button btnPreview;
        private Label lblRed;
        private Label lblGreen;
        private Label lblBlue;
        private TextBox tbRed;
        private TextBox tbBlue;
        private TextBox tbGreen;
        private Button btnCancel;
        private Button btnApply;
    }
}