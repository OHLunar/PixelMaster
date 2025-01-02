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
    public partial class NewFileForm : Form // Form for new file creation
    {
        public int PageWidth { get; private set; }
        public int PageHeight { get; private set; }

        public NewFileForm()
        {
            InitializeComponent();
            CheckForClipboard();
        }

        // This function checks if the clipboard contains an image and retrieves its dimensions (width and height).
        // It then updates the corresponding text fields and sets the PageWidth and PageHeight variables.
        private void CheckForClipboard()
        {
            if (Clipboard.ContainsImage())
            {
                tbWidth.Text = Clipboard.GetImage().Width.ToString();
                tbHeight.Text = Clipboard.GetImage().Height.ToString();
                PageWidth = int.Parse(tbWidth.Text);
                PageHeight = int.Parse(tbHeight.Text);
            }
        }

        // This function is triggered when the user clicks the "Create File" button.
        // It validates the entered width and height, ensuring they are valid integers and positive values,
        // and then sets the PageWidth and PageHeight if valid.
        private void btnCreateFile_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(tbWidth.Text, out int width) && int.TryParse(tbHeight.Text, out int height))
            {
                // Validate dimensions if needed (e.g., non-negative)
                if (width > 0 && height > 0)
                {
                    PageWidth = width;
                    PageHeight = height;
                    this.DialogResult = DialogResult.OK; // Close form and indicate success
                }
                else
                {
                    MessageBox.Show("Width and height must be positive integers.");
                }
            }
            else
            {
                MessageBox.Show("Please enter valid integers for width and height.");
            }
        }
    }
}
