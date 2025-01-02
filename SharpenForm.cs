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
    public partial class SharpenForm : Form // Form for sharpen filter window
    {
        PaintForm paintForm;
        private Bitmap originalImage, transformedImage;
        private float intensity = 10f;
        private Filter filter;

        public SharpenForm(PaintForm paintForm) // Constructor
        {
            InitializeComponent();
            this.paintForm = paintForm;
            originalImage = paintForm.paintBoardImage;
            transformedImage = paintForm.paintBoardImage;
            pictureboxOriginal.Image = originalImage;
            pictureboxTransformed.Image = transformedImage;
            pictureboxOriginal.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureboxTransformed.SizeMode = PictureBoxSizeMode.StretchImage;
            tbScale.Text = trackbarScale.Value.ToString();
            filter = new Filter();
        }

        // Updates the intensity value based on the trackbar position and displays it in a text box
        private void trackbarScale_Scroll(object sender, EventArgs e)
        {
            intensity = ((TrackBar)sender).Value;
            tbScale.Text = ((int)intensity).ToString();
        }

        // Applies a sharpen filter to the original image with the selected intensity and displays a preview
        private void btnPreview_Click(object sender, EventArgs e)
        {
            transformedImage = filter.SharpenImage(originalImage, intensity);
            pictureboxTransformed.Image = transformedImage;
        }

        // Applies the transformed image to the main PaintBoard in the paint form and closes the dialog
        private void btnApply_Click(object sender, EventArgs e)
        {
            paintForm.SetPaintBoardImage(transformedImage);
            Close();
        }

        // Closes the dialog without applying changes
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
