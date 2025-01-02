namespace PixelMaster
{
    partial class ResizeForm
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
            lblWidth = new Label();
            lblHeight = new Label();
            chkMaintainAspectRatio = new CheckBox();
            btnResize = new Button();
            numWidth = new TextBox();
            numHeight = new TextBox();
            SuspendLayout();
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Location = new Point(169, 25);
            lblWidth.Margin = new Padding(2, 0, 2, 0);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(39, 15);
            lblWidth.TabIndex = 2;
            lblWidth.Text = "Width";
            // 
            // lblHeight
            // 
            lblHeight.AutoSize = true;
            lblHeight.Location = new Point(169, 67);
            lblHeight.Margin = new Padding(2, 0, 2, 0);
            lblHeight.Name = "lblHeight";
            lblHeight.Size = new Size(43, 15);
            lblHeight.TabIndex = 3;
            lblHeight.Text = "Height";
            // 
            // chkMaintainAspectRatio
            // 
            chkMaintainAspectRatio.AutoSize = true;
            chkMaintainAspectRatio.Location = new Point(26, 105);
            chkMaintainAspectRatio.Margin = new Padding(2);
            chkMaintainAspectRatio.Name = "chkMaintainAspectRatio";
            chkMaintainAspectRatio.Size = new Size(142, 19);
            chkMaintainAspectRatio.TabIndex = 4;
            chkMaintainAspectRatio.Text = "Maintain Aspect Ratio";
            chkMaintainAspectRatio.TextAlign = ContentAlignment.MiddleCenter;
            chkMaintainAspectRatio.UseVisualStyleBackColor = true;
            // 
            // btnResize
            // 
            btnResize.Location = new Point(58, 168);
            btnResize.Margin = new Padding(2);
            btnResize.Name = "btnResize";
            btnResize.Size = new Size(119, 31);
            btnResize.TabIndex = 5;
            btnResize.Text = "Resize";
            btnResize.UseVisualStyleBackColor = true;
            // 
            // numWidth
            // 
            numWidth.Location = new Point(26, 23);
            numWidth.Margin = new Padding(2);
            numWidth.Name = "numWidth";
            numWidth.Size = new Size(140, 23);
            numWidth.TabIndex = 6;
            numWidth.KeyPress += numWidth_KeyPress;
            // 
            // numHeight
            // 
            numHeight.Location = new Point(26, 65);
            numHeight.Margin = new Padding(2);
            numHeight.Name = "numHeight";
            numHeight.Size = new Size(140, 23);
            numHeight.TabIndex = 7;
            numHeight.KeyPress += numHeight_KeyPress;
            // 
            // ResizeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(248, 206);
            Controls.Add(numHeight);
            Controls.Add(numWidth);
            Controls.Add(btnResize);
            Controls.Add(chkMaintainAspectRatio);
            Controls.Add(lblHeight);
            Controls.Add(lblWidth);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(2);
            Name = "ResizeForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Resize Image";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblWidth;
        private Label lblHeight;
        private CheckBox chkMaintainAspectRatio;
        private Button btnResize;
        private TextBox numWidth;
        private TextBox numHeight;
    }
}