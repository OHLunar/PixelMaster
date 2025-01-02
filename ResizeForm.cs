using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelMaster
{
    public partial class ResizeForm : Form // Form for resize window
    {
        PaintForm paintForm;
        private Bitmap originalImage;
        public Bitmap ResizedImage { get; private set; }
        private float aspectRatio;

        public ResizeForm(Bitmap image) // Constructor
        {
            this.paintForm = paintForm;
            originalImage = image;
            ResizedImage = (Bitmap)image.Clone();
            aspectRatio = (float)image.Width / image.Height;
            InitializeComponent();

            // Initialize controls
            numWidth.Text = originalImage.Width.ToString();
            numHeight.Text = originalImage.Height.ToString();
            chkMaintainAspectRatio.Checked = true;

            // Add event handlers
            numWidth.TextChanged += NumWidth_ValueChanged;
            numHeight.TextChanged += NumHeight_ValueChanged;
            btnResize.Click += BtnResize_Click;
        }

        // This function is triggered when the width value is changed.
        // It maintains the aspect ratio by adjusting the height accordingly when the user modifies the width.
        private void NumWidth_ValueChanged(object sender, EventArgs e)
        {
            if (chkMaintainAspectRatio.Checked)
            {
                // Adjust height based on the new width to maintain aspect ratio
                if (!numWidth.Text.Equals(String.Empty))
                {
                    numHeight.Text = ((int)Math.Round(float.Parse(numWidth.Text) / aspectRatio)).ToString();
                }
                else
                    numHeight.Text = String.Empty;
            }
            if (!numWidth.Text.Equals(String.Empty) && int.Parse(numWidth.Text) > 5000)
            {
                MessageBox.Show("Value too large!");
                numWidth.Text = (int.Parse(numWidth.Text) / 10).ToString();
            }
        }

        // This function is triggered when the height value is changed.
        // It maintains the aspect ratio by adjusting the width accordingly when the user modifies the height.
        private void NumHeight_ValueChanged(object sender, EventArgs e)
        {
            if (chkMaintainAspectRatio.Checked)
            {
                if (!numHeight.Text.Equals(String.Empty))
                {
                    numWidth.Text = ((int)Math.Round(float.Parse(numHeight.Text) * aspectRatio)).ToString();
                }
                else
                    numWidth.Text = String.Empty;
            }

            if (!numHeight.Text.Equals(String.Empty) && int.Parse(numHeight.Text) > 5000)
            {
                MessageBox.Show("Value too large!");
                numHeight.Text = (int.Parse(numHeight.Text) / 10).ToString();
            }
        }

        // This function is called when the resize button is clicked.
        // It resizes the image to the new width and height, using high-quality bicubic interpolation.
        private void BtnResize_Click(object sender, EventArgs e)
        {
            if (!numWidth.Text.Equals(String.Empty) && !numHeight.Text.Equals(String.Empty) && int.Parse(numWidth.Text) > 0 && int.Parse(numHeight.Text) > 0)
            {
                int newWidth = int.Parse(numWidth.Text);
                int newHeight = int.Parse(numHeight.Text);

                ResizedImage = new Bitmap(newWidth, newHeight);

                using (Graphics g = Graphics.FromImage(ResizedImage))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight));
                }

                originalImage = (Bitmap)ResizedImage.Clone();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Invalid Values!");
        }

        // This function handles keypress events for the width input field.
        // It prevents non-numeric input and plays an exclamation sound when invalid input is detected.
        private void numWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        // This function handles keypress events for the height input field.
        // It prevents non-numeric input and plays an exclamation sound when invalid input is detected.
        private void numHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Exclamation.Play();
            }
        }
    }
}
