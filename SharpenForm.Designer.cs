﻿namespace PixelMaster
{
    partial class SharpenForm
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
            btnPreview = new Button();
            trackbarScale = new TrackBar();
            tbScale = new TextBox();
            lblScale = new Label();
            btnApply = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureboxOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureboxTransformed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackbarScale).BeginInit();
            SuspendLayout();
            // 
            // pictureboxOriginal
            // 
            pictureboxOriginal.BackgroundImageLayout = ImageLayout.None;
            pictureboxOriginal.Location = new Point(115, 117);
            pictureboxOriginal.Name = "pictureboxOriginal";
            pictureboxOriginal.Size = new Size(408, 518);
            pictureboxOriginal.TabIndex = 1;
            pictureboxOriginal.TabStop = false;
            // 
            // pictureboxTransformed
            // 
            pictureboxTransformed.BackgroundImageLayout = ImageLayout.None;
            pictureboxTransformed.Location = new Point(700, 117);
            pictureboxTransformed.Name = "pictureboxTransformed";
            pictureboxTransformed.Size = new Size(408, 518);
            pictureboxTransformed.TabIndex = 2;
            pictureboxTransformed.TabStop = false;
            // 
            // btnPreview
            // 
            btnPreview.Location = new Point(839, 722);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(140, 50);
            btnPreview.TabIndex = 10;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // trackbarScale
            // 
            trackbarScale.Location = new Point(115, 703);
            trackbarScale.Maximum = 50;
            trackbarScale.Minimum = 10;
            trackbarScale.Name = "trackbarScale";
            trackbarScale.Size = new Size(352, 69);
            trackbarScale.TabIndex = 11;
            trackbarScale.Value = 50;
            trackbarScale.Scroll += trackbarScale_Scroll;
            // 
            // tbScale
            // 
            tbScale.Location = new Point(476, 703);
            tbScale.Name = "tbScale";
            tbScale.Size = new Size(47, 31);
            tbScale.TabIndex = 14;
            // 
            // lblScale
            // 
            lblScale.AutoSize = true;
            lblScale.Location = new Point(277, 675);
            lblScale.Name = "lblScale";
            lblScale.Size = new Size(79, 25);
            lblScale.TabIndex = 15;
            lblScale.Text = "Intensity";
            // 
            // btnApply
            // 
            btnApply.Location = new Point(1133, 999);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(140, 50);
            btnApply.TabIndex = 18;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(987, 999);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 50);
            btnCancel.TabIndex = 19;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // SharpenForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1285, 1061);
            Controls.Add(btnCancel);
            Controls.Add(btnApply);
            Controls.Add(lblScale);
            Controls.Add(tbScale);
            Controls.Add(trackbarScale);
            Controls.Add(btnPreview);
            Controls.Add(pictureboxTransformed);
            Controls.Add(pictureboxOriginal);
            Name = "SharpenForm";
            Text = "Sharpen";
            ((System.ComponentModel.ISupportInitialize)pictureboxOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureboxTransformed).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackbarScale).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureboxOriginal;
        private PictureBox pictureboxTransformed;
        private Button btnPreview;
        private TrackBar trackbarScale;
        private TextBox tbScale;
        private Label lblScale;
        private Button btnApply;
        private Button btnCancel;
    }
}