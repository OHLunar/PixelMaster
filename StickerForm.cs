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
    public partial class StickerForm : Form // Form for sticker window
    {
        public Image SelectedSticker { get; private set; }

        public StickerForm() // Constructor
        {
            InitializeComponent();

            this.Text = "Select a Sticker";
            this.Size = new Size(300, 400);

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            this.Controls.Add(panel);

            // Load sticker images and add them to the panel as clickable PictureBox controls
            List<Image> stickers = LoadStickers();
            foreach (var sticker in stickers)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Image = sticker,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(panel.Width/3 - 15, panel.Width/3 - 15),
                    Margin = new Padding(5)
                };
                pictureBox.Click += (s, e) => SelectSticker(sticker);
                panel.Controls.Add(pictureBox);
            }
        }

        // Loads a predefined list of sticker images from resources
        private List<Image> LoadStickers() 
        {
            return new List<Image>
            {
                Properties.Resources.clown,
                Properties.Resources.skull,
                Properties.Resources.fish,
                Properties.Resources.angrySticker,
                Properties.Resources.coolSticker,
                Properties.Resources.deadSticker,
                Properties.Resources.smileSticker,
                Properties.Resources.vomitingSticker,
                Properties.Resources.winkSticker,
                Properties.Resources.tongueSticker,
                Properties.Resources.sadSticker,
                Properties.Resources.confusedSticker,
            };
        }

        // Sets the selected sticker, updates dialog result, and closes the form
        private void SelectSticker(Image sticker)
        {
            SelectedSticker = sticker;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
