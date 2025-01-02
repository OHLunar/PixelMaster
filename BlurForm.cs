using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelMaster
{
    public partial class BlurForm : Form // Blur window form
    {
        private PaintForm paintForm;
        private Bitmap originalImage, transformedImage;
        private Filter filter;
        private int size = 5;

        public BlurForm(PaintForm paintForm) // Constructor
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

        private void trackbarScale_Scroll(object sender, EventArgs e) // Trackbar for selecting the intensity
        {
            size = ((TrackBar)sender).Value;
            tbScale.Text = size.ToString();
        }

        private void btnPreview_Click(object sender, EventArgs e) // Preview Button
        {
            transformedImage = filter.ApplyBlur(originalImage, size);
            pictureboxTransformed.Image = transformedImage;
        }

        private void btnApply_Click(object sender, EventArgs e) // Apply Button
        {
            paintForm.SetPaintBoardImage(transformedImage);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) // Cancel Button
        {
            Close();
        }
    }
}
