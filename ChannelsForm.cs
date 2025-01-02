using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace PixelMaster
{
    public partial class ChannelsForm : Form // Form for Channel Mixer window
    {
        PaintForm paintForm;
        private Bitmap originalImage, transformedImage;
        private int red = 100, green = 100, blue = 100;
        private Colors colors;

        public ChannelsForm(PaintForm paintForm) // Constructor
        {
            InitializeComponent();
            this.paintForm = paintForm;
            originalImage = paintForm.paintBoardImage;
            transformedImage = paintForm.paintBoardImage;
            pictureboxOriginal.Image = originalImage;
            pictureboxTransformed.Image = transformedImage;
            pictureboxOriginal.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureboxTransformed.SizeMode = PictureBoxSizeMode.StretchImage;
            colors = new Colors();

            tbRed.Text = red.ToString();
            tbGreen.Text = green.ToString();
            tbBlue.Text = blue.ToString();
        }

        private void trackbarRed_Scroll(object sender, EventArgs e) // Trackbar for Red channel
        {
            red = ((TrackBar)sender).Value;
            tbRed.Text = red.ToString();
        }

        private void trackbarBlue_Scroll(object sender, EventArgs e) // Trackbar for Blue channel
        {
            blue = ((TrackBar)sender).Value;
            tbBlue.Text = blue.ToString();
        }

        private void trackbarGreen_Scroll(object sender, EventArgs e) // Trackbar for Green channel
        {
            green = ((TrackBar)sender).Value;
            tbGreen.Text = green.ToString();
        }

        private void btnPreview_Click(object sender, EventArgs e) // Preview button
        {
            transformedImage = colors.ChangeChannels(originalImage, blue, green, red);
            pictureboxTransformed.Image = transformedImage;
        }

        private void tbRed_TextChanged(object sender, EventArgs e) // Event for change in red channel textbox value
        {
            if (int.TryParse(tbRed.Text, out red))
            {
                if (red < 0)
                    red = 0;
                else if (red > 100)
                    red = 100;

                trackbarRed.Value = red;
            }
        }

        private void tbBlue_TextChanged(object sender, EventArgs e) // Event for change in blue channel textbox value
        {
            if (int.TryParse(tbBlue.Text, out blue))
            {
                if (blue < 0)
                    blue = 0;
                else if (blue > 100)
                    blue = 100;

                trackbarBlue.Value = blue;
            }
        }

        private void tbGreen_TextChanged(object sender, EventArgs e) // Event for change in green channel textbox value
        {
            if (int.TryParse(tbGreen.Text, out green))
            {
                if (green < 0)
                    green = 0;
                else if (green > 100)
                    green = 100;

                trackbarGreen.Value = green;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) // Cancel button
        {
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e) // Apply button
        {
            paintForm.SetPaintBoardImage(transformedImage);
            Close();
        }
    }
}
