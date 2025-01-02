using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection.Metadata.Ecma335;


namespace PixelMaster
{
    internal class Palette // Form for pallete toolbar
    {
        PaintForm paintForm;
        private Panel panel;
        private ToolStrip toolStrip;
        private ToolStripButton[] paletteGrid;
        private ToolStripButton custom;
        private ToolStripButton selectedColor;
        private Color color;

        public Palette(PaintForm paintForm, Panel panel) // Constructor
        {
            this.paintForm = paintForm;
            this.panel = panel;
            this.color = Color.Black;

            // Initialize ToolStrip and ToolStripButtons
            toolStrip = new ToolStrip
            {
                //AutoSize = false,
                LayoutStyle = ToolStripLayoutStyle.Flow,
                Dock = DockStyle.Top,
                BackColor = SystemColors.ControlLight,
                Padding = new Padding(3, 0, 0, 0)
            };

            paletteGrid = new ToolStripButton[32];
            LoadPalette(toolStrip);
            // Add ToolStrip to Panel
            panel.Controls.Add(toolStrip);
        }

        private void LoadPalette(ToolStrip toolstrip) // Creates and loads Pallete into given Toolstrip
        {   
            int red = 0, green = 0, blue = 0;
            int index = 0;

            // Determine the size of each button based on the panel width
            int buttonSize = (panel.Width - 15) / 3; // Subtracting padding and dividing by 3 columns

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    paletteGrid[index] = new ToolStripButton
                    {
                        DisplayStyle = ToolStripItemDisplayStyle.Text,
                        BackColor = Color.FromArgb(red, green, blue),
                        Font = new Font("Segoe UI", 11),
                        Text = " ",
                        ToolTipText = $"{red} {green} {blue}",
                        Margin = new Padding(2), // Smaller margin to fit the panel
                        AutoSize = false,
                        Size = new Size(buttonSize, buttonSize)
                    };
                    paletteGrid[index].MouseDown += new MouseEventHandler(SetColor);
                    toolStrip.Items.Add(paletteGrid[index]);

                    if (blue == 0)
                        blue = 128;
                    else if (blue == 128)
                        blue = 255;
                    else
                        blue = 0;

                    index++;

                    if (index % 9 == 0)
                    {
                        if (red == 0)
                            red = 128;
                        else if (red == 128)
                            red = 255;
                        else
                            red = 0;
                    }
                }

                if (green == 0)
                    green = 128;
                else if (green == 128)
                    green = 255;
                else
                    green = 0;
            }

            selectedColor = new ToolStripButton
            {
                Text = "",
                Margin = new Padding(1),
                AutoSize = false,
                Size = new Size(buttonSize * 3 + 9, buttonSize * 3 + 9),
                BackColor = color,
                Enabled = false
            };
            toolStrip.Items.Add(selectedColor);

            custom = new ToolStripButton
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Text = "Custom",
                Margin = new Padding(1),
                AutoSize = false,
                Size = new Size(buttonSize * 3 + 9, buttonSize)
            };
            custom.MouseDown += new MouseEventHandler(CustomColor);
            toolStrip.Items.Add(custom);
        }

        private void SetColor(object? sender, MouseEventArgs e) // Sets brush color to color picked from pallete
        {
            ToolStripButton? btn = sender as ToolStripButton;
            if (btn != null)
            {
                color = btn.BackColor;
                paintForm.SetBrushColor(color);
                selectedColor.BackColor = color;
            }
        }

        private void CustomColor(object? sender, EventArgs e) // Functionality of "Custom" color button
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog.Color;
                paintForm.SetBrushColor(color);
                selectedColor.BackColor = color;
            }
        }

        public ToolStrip GetToolStrip()
        {
            return toolStrip;
        }

        public Color GetColor()
        {
            return this.color;
        }

        public void UpdateSelectedColor(Color newColor) // Updates selected color
        {
            color = newColor;
            selectedColor.BackColor = newColor;
        }
    }
}