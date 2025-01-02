namespace PixelMaster
{
    partial class NewFileForm
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
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            btnCreateFile = new Button();
            lblWidth = new Label();
            lblHeight = new Label();
            tbWidth = new TextBox();
            tbHeight = new TextBox();
            SuspendLayout();
            // 
            // btnCreateFile
            // 
            btnCreateFile.Location = new Point(55, 80);
            btnCreateFile.Name = "btnCreateFile";
            btnCreateFile.Size = new Size(75, 23);
            btnCreateFile.TabIndex = 0;
            btnCreateFile.Text = "Create";
            btnCreateFile.UseVisualStyleBackColor = true;
            btnCreateFile.Click += btnCreateFile_Click_1;
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(10, 15);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(39, 15);
            lblWidth.TabIndex = 1;
            lblWidth.Text = "Width";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(10, 45);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(43, 15);
            lblHeight.TabIndex = 2;
            lblHeight.Text = "Height";
            // 
            // tbWidth
            // 
            tbWidth.Location = new Point(75, 15);
            tbWidth.Name = "tbWidth";
            tbWidth.Size = new Size(100, 23);
            tbWidth.TabIndex = 3;
            // 
            // tbHeight
            // 
            tbHeight.Location = new Point(75, 45);
            tbHeight.Name = "tbHeight";
            tbHeight.Size = new Size(100, 23);
            tbHeight.TabIndex = 4;
            // 
            // NewFileForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(184, 111);
            Controls.Add(tbHeight);
            Controls.Add(tbWidth);
            Controls.Add(lblHeight);
            Controls.Add(lblWidth);
            Controls.Add(btnCreateFile);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "NewFileForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New File";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Button btnCreateFile;
        private Label lblWidth;
        private Label lblHeight;
        private TextBox tbWidth;
        private TextBox tbHeight;
    }
}