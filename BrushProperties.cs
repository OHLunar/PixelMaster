using PixelMaster.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PixelMaster
{
    internal class BrushProperties
    {
        private PaintForm paintForm;
        private Panel panel;
        private TrackBar trackBar, outlineSize;
        private TextBox textBox, outlineValue;
        private CheckBox checkboxOutline;
        private ToolStrip shapeToolStrip, selectionToolStrip, textToolStrip, stickerToolStrip;
        private ToolStripDropDownButton shapeDropDownButton;
        private ToolStripButton buttonCut, buttonCopy, buttonPaste, outlineColor;
        private Label label;
        private int brushSize;
        private string selectedShape = "Circle";
        Bitmap croppedImage;

        public BrushProperties(PaintForm paintForm, Panel panel) // constructor
        {
            this.paintForm = paintForm;
            this.panel = panel;
        }

        public ToolStripButton getCutButton() => buttonCut;
        public ToolStripButton getCopyButton() => buttonCopy;
        public ToolStripButton getPasteButton() => buttonPaste;

        private void LoadDrawToolbar() // Loads Brush Properties toolbar after selecting brush\eraser tool
        {
            // Initialize Label
            label = new Label
            {
                Text = "Brush Size:",
                AutoSize = true,
                Location = new Point(150, 25)
            };

            // Initialize TrackBar
            trackBar = new TrackBar
            {
                Minimum = 1,
                Maximum = 100,
                Value = 10,
                TickFrequency = 5,
                SmallChange = 1,
                LargeChange = 5,
                Location = new Point(250, 20),
                Width = 200
            };
            trackBar.ValueChanged += TrackBar_ValueChanged;

            // Initialize TextBox
            textBox = new TextBox
            {
                Text = trackBar.Value.ToString(),
                Location = new Point(460, 20),
                Width = 50
            };
            textBox.TextChanged += TextBox_TextChanged;

            // Add controls to Panel
            panel.Controls.Add(label);
            panel.Controls.Add(trackBar);
            panel.Controls.Add(textBox);
        }

        private void LoadBrushToolbar() // Loads brush properties toolbar with shape selection dropdown menu
        {
            LoadDrawToolbar();

            shapeToolStrip = new ToolStrip
            {
                LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow,
                AutoSize = true,
                Size = new Size(100, 50),
                Dock = DockStyle.None,
                Location = new Point(520, 20),
                GripStyle = ToolStripGripStyle.Hidden,
            };

            // Add ToolStripDropDownButton for shape selection
            shapeDropDownButton = new ToolStripDropDownButton
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = Resources.round,
                Size = new Size(50, 50)
            };
            shapeDropDownButton.DropDownItems.Add("Round", null, (sender, e) => SetShape("Circle"));
            shapeDropDownButton.DropDownItems.Add("Square", null, (sender, e) => SetShape("Rectangle"));
            shapeToolStrip.Items.Add(shapeDropDownButton);
            panel.Controls.Add(shapeToolStrip);
        }

        private void LoadTextToolbar() // Loads text properties toolbar once text tool is selected
        {
            textToolStrip = new ToolStrip
            {
                LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow,
                AutoSize = true,
                Dock = DockStyle.None,
                Location = new Point(150, 20),
                GripStyle = ToolStripGripStyle.Hidden,
                BackColor = SystemColors.ControlLight
            };

            checkboxOutline = new CheckBox
            {
                Text = "Outline",
                Location = new Point(300, 25)
            };
            checkboxOutline.CheckedChanged += Outline_CheckChanged;

            outlineColor = new ToolStripButton
            {
                Text = "Color",
                Enabled = false,
            };
            outlineColor.Click += outlineColor_Click;

            outlineSize = new TrackBar
            {
                Minimum = 1,
                Maximum = 20,
                Value = 1,
                TickFrequency = 1,
                SmallChange = 1,
                LargeChange = 5,
                Location = new Point(400, 25),
                Width = 200,
                Visible = false
            };
            outlineSize.ValueChanged += OutlineSize_ValueChanged;


            outlineValue = new TextBox
            {
                Size = new Size(47, 31),
                Location = new Point(600, 25),
                Text = "1",
                Visible = false
            };
            outlineValue.TextChanged += OutlineValue_TextChanged;
            textToolStrip.Items.Add("Font", Resources.text, (sender, e) => paintForm.PickFont());
            textToolStrip.Items.Add(outlineColor);
            textToolStrip.Renderer = new CustomToolstrip();
            panel.Controls.Add(textToolStrip);
            panel.Controls.Add(checkboxOutline);
            panel.Controls.Add(outlineSize);
            panel.Controls.Add(outlineValue);
        }

        private void OutlineValue_TextChanged(object? sender, EventArgs e) // Event for changing value in textbox for outline
        {
            if (int.Parse(outlineValue.Text) < 1 || int.Parse(outlineValue.Text) > 20)
            {
                MessageBox.Show("Invalid Value!");
                outlineValue.Text = outlineSize.Value.ToString();
            }
            else
                outlineSize.Value = int.Parse(outlineValue.Text);
        }

        private void OutlineSize_ValueChanged(object? sender, EventArgs e) // Event for changing the value of outline trackbar
        {
            paintForm.OutlineSize = outlineSize.Value;
            outlineValue.Text = outlineSize.Value.ToString();
        }

        private void outlineColor_Click(object? sender, EventArgs e) // Event for clicking the outline color button
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                paintForm.OutlineColor = colorDialog.Color;
            }
        }

        private void Outline_CheckChanged(object? sender, EventArgs e) // Event for checking the outline checkbox
        {
            if (checkboxOutline.Checked)
            {
                paintForm.IsOutline = true;
                outlineColor.Enabled = true;
                outlineSize.Visible = true;
                outlineValue.Visible = true;
            }
            else
            {
                paintForm.IsOutline = false;
                outlineColor.Enabled = false;
                outlineSize.Visible = false;
                outlineValue.Visible = false;
            }
        }

        private void LoadSelectionToolbar() // Loads selection properties toolbar once selection tool is selected
        {
            selectionToolStrip = new ToolStrip
            {
                LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow,
                AutoSize = true,
                Dock = DockStyle.None,
                Location = new Point(150, 20),
                GripStyle = ToolStripGripStyle.Hidden,
                BackColor = SystemColors.ControlLight
            };

            buttonCut = new ToolStripButton
            {
                DisplayStyle= ToolStripItemDisplayStyle.ImageAndText,
                Image = Resources.cut,
                Text = "Cut",
                Enabled = false
            };

            buttonCut.Click += ButtonCut_Click;

            buttonCopy = new ToolStripButton
            {
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                Image = Resources.copy,
                Text = "Copy",
                Enabled = false
            };
            buttonCopy.Click += ButtonCopy_Click;

            buttonPaste = new ToolStripButton
            {
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                Image = Resources.paste,
                Text = "Paste"
            };
            buttonPaste.Click += ButtonPaste_Click;


            selectionToolStrip.Items.Add(buttonCut);
            selectionToolStrip.Items.Add(buttonCopy);
            selectionToolStrip.Items.Add(buttonPaste);
            selectionToolStrip.Renderer = new CustomToolstrip();
            panel.Controls.Add(selectionToolStrip);
        }

        private void ButtonPaste_Click(object? sender, EventArgs e) // Event for clicking Paste button when selection tool is selected
        {
            if (Clipboard.ContainsImage())
            {
                Bitmap buffer = Clipboard.GetImage() as Bitmap;
                paintForm.PasteOnImage(paintForm.paintBoardImage, buffer, new Point(0, 0));
            }
            buttonCut.Enabled = false;
            buttonCopy.Enabled = false;
        }

        private void ButtonCut_Click(object? sender, EventArgs e) // Event for clicking Cut button when selection tool is selected
        {
            croppedImage = paintForm.CropArea(0);
            buttonCut.Enabled = false;
            buttonCopy.Enabled = false;
        }

        private void ButtonCopy_Click(object? sender, EventArgs e) // Event for clicking Copy button when selection tool is selected
        {
            croppedImage = paintForm.CropArea(1);
            buttonCut.Enabled = false;
            buttonCopy.Enabled = false;
        }

        public void LoadToolbar(int tool) // Function to switch between toolbars when relevant tool is picked
        {
            if (panel.Controls.Count > 0) 
                panel.Controls.Clear();

            switch (tool)
            {
                case 1:
                case 2:
                    LoadBrushToolbar();
                    break;

                case 5:
                case 6:
                case 7:
                    LoadDrawToolbar();
                    break;

                case 8:
                    LoadTextToolbar();
                    break;

                case 9:
                    LoadSelectionToolbar();
                    break;

                case 11:
                    LoadStickerToolbar();
                    break;

                default:
                    break;
            }
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e) // Event for changing the value of brush size when its trackbar value is changed
        {
            brushSize = trackBar.Value;
            textBox.Text = brushSize.ToString();
            paintForm.SetBrushSize(brushSize);
        }

        private void TextBox_TextChanged(object sender, EventArgs e) // Event for changing value in textbox related to brush size
        {
            if (int.TryParse(textBox.Text, out brushSize))
            {
                if (brushSize < trackBar.Minimum)
                    brushSize = trackBar.Minimum;
                else if (brushSize > trackBar.Maximum)
                    brushSize = trackBar.Maximum;

                trackBar.Value = brushSize;
                paintForm.SetBrushSize(brushSize);
            }
        }

        private void SetShape(string shape) // Function to set shape of a brush\eraser to circle or square
        {
            if (shape == "Circle")
                shapeDropDownButton.Image = Resources.round;
            else
                shapeDropDownButton.Image = Resources.square;
            selectedShape = shape;
            paintForm.SetBrushShape(shape);
        }

        private void LoadStickerToolbar() // Loads sticker toolbar when sticker tool is selected3e4
        {
            stickerToolStrip = new ToolStrip
            {
                LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow,
                AutoSize = true,
                Dock = DockStyle.None,
                Location = new Point(150, 20),
                GripStyle = ToolStripGripStyle.Hidden,
                BackColor = SystemColors.ControlLight
            };

            stickerToolStrip.Items.Add("Stickers", Resources.stickerpng, (sender, e) => paintForm.PickSticker());

            var uploadButton = new ToolStripButton
            {
                Image = Resources.upload,
                Text = "Upload Sticker",
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            };
            uploadButton.Click += (sender, e) => paintForm.UploadSticker();

            stickerToolStrip.Items.Add(uploadButton);
            stickerToolStrip.Renderer = new CustomToolstrip();
            panel.Controls.Add(stickerToolStrip);
        }
    }
}