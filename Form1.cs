using System.Drawing;
using System.Text;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System;
using System.Net;
using System.Drawing.Drawing2D;
using System.Reflection;
using PixelMaster.Properties;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace PixelMaster
{
    public partial class PaintForm : Form
    {
        // Fields related to Drawing
        public Bitmap paintBoardImage;
        private bool isImageLoaded = false;
        Bitmap backupImage;
        private Graphics graph;
        private bool isDrawing = false;
        private bool isTransparent = false;
        ShapeFiller shapeFiller;

        // Fields related to Cropping
        private bool isCropping = false;
        private Point startPoint;
        private Point endPoint;
        private Rectangle cropRectangle;
        Bitmap? loadImage;
        Image stickerImage;

        // Fields related to Mouse Interaction
        private bool isMouseDown = false;
        private bool isSelected = false;
        private bool isDragging = false;
        private bool movementDone = false;
        private bool isStickerSelected = false;
        private Point currentPoint;
        private Point offset;
        private Rectangle drawingRectangle;

        // Fields related to Brush Settings
        private int brushSize = 10;
        private string brushShape = "Circle"; // Default shape
        private Color brushColor = Color.Black;
        private Color outlineColor = Color.Black;
        private float outlineSize = 2f;

        private List<Point> pathPoints = new List<Point>();

        // Pens
        private Pen brush;
        private Pen eraser = new Pen(Color.White, 3);

        // Tools
        private int tool;
        private Filter filter;
        private ToolStripButton[] stripButtons;
        private MagicWandTool magicWandTool;

        // Fields related to Text
        private string currentText = "";
        private Color fontColor = Color.Black;

        // Palette and Brush Properties
        private Palette palette;
        private BrushProperties brushProperties;
        private Colors colors;

        // Coordinates for some operations
        Point px, py;
        int x, y, sX, sY, cX, cY;

        // TextTool
        private Font textFont = new Font("Arial", 12);
        private string enteredText = string.Empty;
        private Point textPosition;
        private Rectangle textRect = Rectangle.Empty;
        private bool isWriting = false;

        // Forms
        AboutForm aboutForm;

        public PaintForm() // Constructor
        {
            InitializeComponent();

            // Form Setup
            this.KeyPreview = true;  // Ensure the form receives key events first
            this.KeyDown += new KeyEventHandler(PaintForm_KeyDown);

            // Brush Initialization
            brush = new Pen(brushColor, brushSize);
            filter = new Filter();
            aboutForm = new AboutForm();

            // Paint Board Initialization
            paintBoardImage = new Bitmap(PaintBoard.Width, PaintBoard.Height, PixelFormat.Format32bppArgb);
            backupImage = new Bitmap(PaintBoard.Width, PaintBoard.Height, PixelFormat.Format32bppArgb);
            graph = Graphics.FromImage(paintBoardImage);
            PaintBoard.Image = paintBoardImage;

            using (Graphics g = Graphics.FromImage(paintBoardImage))
            {
                g.Clear(Color.Transparent);
            }

            // Palette and Brush Properties Initialization
            palette = new Palette(this, panel1);
            panel1.Controls.Add(palette.GetToolStrip());

            brushProperties = new BrushProperties(this, panel2);

            // Additional Tools Initialization
            colors = new Colors();
            shapeFiller = new ShapeFiller();
            magicWandTool = new MagicWandTool();

            // Toolstrip Buttons Setup
            stripButtons = new ToolStripButton[]
            {
                btnBrush,
                btnEraser,
                btnFill,
                btnColorPicker,
                btnDrawLine,
                btnDrawRectangle,
                btnDrawEllipse,
                textDrawer,
                btnClearBoard,
                btnSelect,
                btnMagicWand,
                btnStickers
            };
        }

        public bool IsOutline { get; set; }

        public Color OutlineColor { get { return outlineColor; } set { outlineColor = value; } }

        public float OutlineSize { get { return outlineSize; } set { outlineSize = value; } }

        public TextBox GetTextToolTextBox() { return textToolTextBox; }

        public void SetPaintBoardImage(Bitmap image) // Sets parameter bitmap as PaintBoard picturebox image
        {
            paintBoardImage = image;
            PaintBoard.Image = paintBoardImage;
            graph = Graphics.FromImage(paintBoardImage);
        }

        // ============== Tool Selection & Keyboard Shortcut ==============
        private void BtnBrush_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 1, Resources.brush);
        }

        private void BtnEraser_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 2, Resources.eraser);
        }

        private void BtnFill_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 3, Resources.fill);
        }

        private void BtnColorPicker_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 4, Resources.color_picker);

            palette.UpdateSelectedColor(brushColor);
        }

        private void BtnDrawLine_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 5, Resources.diagonal_line);
        }

        private void BtnDrawRectangle_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 6, Resources.unfilled_square);
        }

        private void BtnDrawEllipse_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 7, Resources.circle);
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 9, Resources.selection);
        }

        private void TextDrawer_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 8, Resources.text);
            textToolTextBox.Focus();
        }

        private void BtnClearBoard_Click(object sender, EventArgs e)
        {
            graph.Clear(Color.White);
            PaintBoard.Image = paintBoardImage;

            SetToolCursor(sender, 0, null);
        }

        private void btnMagicWand_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 10, Resources.magic_wand);
        }

        private void BtnStickers_Click(object sender, EventArgs e)
        {
            SetToolCursor(sender, 11, null);
        }

        private void ResetSelectedButton() // Function that unchecks all seleted buttons from tools bar
        {
            foreach (ToolStripButton button in stripButtons)
            {
                button.Checked = false;
            }
        }

        public void SetToolCursor(object sender, int toolType, Bitmap resource) // Function that sets cursor related to selected tool
        {
            tool = toolType;

            if (resource != null)
            {
                Bitmap bm = new Bitmap(resource, 24, 24);
                PaintBoard.Cursor = new Cursor(bm.GetHicon());
            }
            else
            {
                PaintBoard.Cursor = Cursors.Default;
            }

            ResetSelectedButton();
            ((ToolStripButton)sender).Checked = true;
            brushProperties.LoadToolbar(tool);
            Focus();
            textToolTextBox.Focus();
        }

        private void SelectTool(object sender, int toolType, Bitmap resource) // Function that updates the current tool once specific tool is seleted
        {
            SetToolCursor(sender, toolType, resource);
        }

        private void ActivateTextTool() // Function that activates textbox used for text tool
        {
            textToolTextBox.Visible = true;
            textToolTextBox.Focus();
        }

        private void PaintForm_KeyDown(object sender, KeyEventArgs e) // Event that triggers once key is pressed when Main Window is in focus
        {
            if (tool == 9 && e.Control && e.KeyCode == Keys.X)
            {
                brushProperties.getCutButton().PerformClick();
                ResetSelection();
                e.SuppressKeyPress = true;
            }
            if (tool == 9 && e.Control && e.KeyCode == Keys.C)
            {
                brushProperties.getCopyButton().PerformClick();
                ResetSelection();
            }
            if (tool == 9 && e.Control && e.KeyCode == Keys.V)
            {
                brushProperties.getPasteButton().PerformClick();
                ResetSelection();
            }
            else if (!isWriting) // Only allow tool change if is not writing text
            {
                // Key Shortcuts
                if (e.KeyCode == Keys.D1)
                {
                    SelectTool(btnBrush, 1, Resources.brush);
                }
                else if (e.KeyCode == Keys.D2)
                {
                    SelectTool(btnEraser, 2, Resources.eraser);
                }
                else if (e.KeyCode == Keys.D3)
                {
                    SelectTool(btnFill, 3, Resources.fill);
                }
                else if (e.KeyCode == Keys.D4)
                {
                    SelectTool(btnColorPicker, 4, Resources.color_picker);
                }
                else if (e.KeyCode == Keys.D5)
                {
                    SelectTool(btnDrawLine, 5, Resources.diagonal_line);
                }
                else if (e.KeyCode == Keys.D6)
                {
                    SelectTool(btnDrawRectangle, 6, Resources.unfilled_square);
                }
                else if (e.KeyCode == Keys.D7)
                {
                    SelectTool(btnDrawEllipse, 7, Resources.circle);
                }
                else if (e.KeyCode == Keys.D8)
                {
                    SelectTool(btnSelect, 9, Resources.selection);
                }
                else if (e.KeyCode == Keys.T)
                {
                    SelectTool(textDrawer, 8, Resources.text);
                    ActivateTextTool(); // Refactored to encapsulate text tool setup
                }
                else if (e.KeyCode == Keys.M)
                {
                    SelectTool(btnMagicWand, 10, Resources.magic_wand);
                }
                else if (e.KeyCode == Keys.S)
                {
                    SelectTool(btnStickers, 11, null);
                }
            }

            // Common actions that should still work when the text tool is enabled
            if (e.Control && e.KeyCode == Keys.D)
            {
                btnClearBoard.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                newFile.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                openFile.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                saveFile.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                printFile.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                exitFile.PerformClick();
            }
        }


        // ============== PaintBoard Events ==============
        // Handles mouse movement events on the PaintBoard.
        // This function updates the coordinates display, and performs actions based on the current tool and mouse state.
        private void PaintBoard_MouseMove(object sender, MouseEventArgs e)
        {
            // Update the coordinates in the status strip
            coordinatesLabel.Text = $"X: {e.X}, Y: {e.Y}";

            if (isDrawing)
            {
                DrawBrush(e);
                PaintBoard.Invalidate();
            }

            if (isMouseDown && tool == 9)
            {
                movementDone = true;
                currentPoint = e.Location;
                UpdateCropRectangle();
                PaintBoard.Invalidate();
            }

            if (isDragging)
            {
                currentPoint = e.Location;
                MoveCropRectangle();
                PaintBoard.Invalidate();
            }

            x = e.X;
            y = e.Y;
            sX = Math.Abs(x - cX);
            sY = Math.Abs(y - cY);

            if (isStickerSelected)
            {
                currentPoint = new Point(e.X - 25, e.Y - 25);
                PaintBoard.Invalidate();
            }
        }

        // Handles mouse down events on the PaintBoard.
        // Initiates actions based on the current tool and mouse button state.
        private void PaintBoard_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && tool == 9 && !isSelected)
            {
                isMouseDown = true;
                startPoint = e.Location;
            }

            if (isSelected && cropRectangle.Contains(e.Location))
            {
                isDragging = true;
                offset = new Point(e.X - cropRectangle.X, e.Y - cropRectangle.Y);
                PaintBoard.Cursor = Cursors.SizeAll;
            }

            if (isStickerSelected)
            {
                ApplySticker(stickerImage, new Point(e.X - 25, e.Y - 25));
                isStickerSelected = false;
                stickerImage.Dispose();
                PaintBoard.Invalidate();
            }
            isDrawing = true;
            py = e.Location;

            cX = e.X;
            cY = e.Y;
        }

        // Handles mouse up events on the PaintBoard.
        // Completes drawing, cropping, and dragging operations based on the tool and mouse state.
        private void PaintBoard_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

            sX = x - cX;
            sY = y - cY;

            DrawShape(cX, cY, sX, sY);

            if (isMouseDown && tool == 9)
            {
                isMouseDown = false;
                endPoint = e.Location;
                UpdateCropRectangle();

                PaintBoard.Invalidate();
                if (!movementDone)
                    ResetSelection();
                else
                    isSelected = true;
                brushProperties.getCutButton().Enabled = true;
                brushProperties.getCopyButton().Enabled = true;
                movementDone = false;
            }

            if (isDragging)
            {
                isDragging = false;
                PaintBoard.Cursor = Cursors.Default;
            }

            if (isSelected && cropRectangle.Contains(e.Location))
                PaintBoard.Cursor = new Cursor(new Bitmap(Resources.selection, 24, 24).GetHicon());
        }

        // Handles mouse click events on the PaintBoard.
        // Executes actions based on the selected tool, such as fill, color picker, text entry, or magic wand.
        private void PaintBoard_MouseClick(object sender, MouseEventArgs e)
        {
            ShapeFiller sf = new();

            if (tool == 3) // Fill tool
            {
                int threshold = 15;
                List<Point> newPoints;

                SetPaintBoardImage(shapeFiller.FloodFill(paintBoardImage, e.Location, threshold, out newPoints, brushColor));
            }
            if (tool == 4) // Color Picker tool
            {
                if (paintBoardImage != null)
                {
                    brushColor = paintBoardImage.GetPixel(e.X, e.Y);
                    SetBrushColor(brushColor);
                }
            }

            if (isSelected && !isDragging)
            {
                ResetSelection();
            }

            if (tool == 8) // Text tool
            {
                enteredText = string.Empty;
                textToolTextBox.Text = string.Empty;
                textPosition = e.Location;
                this.PaintBoard.Invalidate();
                textToolTextBox.Focus();
                isWriting = true;
            }

            if (tool == 10) // Magicward tool
            {
                int threshold = 7;
                if (paintBoardImage == null) return;

                System.Drawing.Point clickPoint = e.Location;

                List<Point> newPoints;
                Bitmap resultImage = magicWandTool.CreateFilledMask(paintBoardImage, clickPoint, threshold, out newPoints);

                paintBoardImage = (Bitmap)resultImage.Clone();
                graph = Graphics.FromImage(paintBoardImage);
                PaintBoard.Image = paintBoardImage;
            }
        }

        // Handles the Paint event for the PaintBoard.
        // Renders shapes, crop areas, text, stickers, and grid based on the current tool and state.
        private void PaintBoard_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawing)
            {
                int drawX = Math.Min(cX, x);
                int drawY = Math.Min(cY, y);
                UpdateShape(e, drawX, drawY, sX, sY);
            }

            if (tool == 9 && (isMouseDown || cropRectangle != Rectangle.Empty))
            {
                using (Pen dashedPen = new Pen(Color.Red, 4))
                {
                    dashedPen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawRectangle(dashedPen, cropRectangle);
                }
            }

            if (tool == 8 && textPosition != Point.Empty)
            {
                Graphics g = e.Graphics;

                if (!string.IsNullOrEmpty(enteredText))
                {
                    // Measure the size of the current text
                    SizeF textSize = g.MeasureString(enteredText, textFont);

                    // Update the text rectangle based on the current text size
                    textRect = new Rectangle(textPosition.X, textPosition.Y, (int)textSize.Width, (int)textSize.Height);

                    // Draw the text
                    g.DrawString(enteredText, textFont, new SolidBrush(brushColor), textPosition);
                }
                else
                {
                    textRect = new Rectangle(textPosition.X, textPosition.Y, 15, textFont.Height);
                }

                // Draw the dashed rectangle around the text
                using (Pen dashedPen = new Pen(Color.Black))
                {
                    dashedPen.DashStyle = DashStyle.Dash;
                    g.DrawRectangle(dashedPen, textRect);
                }
            }

            if (tool == 11 && isStickerSelected)
            {
                int fixedWidth = 50;
                int fixedHeight = 50;

                Image resizedSticker = ResizeImage(stickerImage, fixedWidth, fixedHeight);


                e.Graphics.DrawImage(resizedSticker, currentPoint);
                PaintBoard.Refresh();
            }

            if (enableGrid.Checked)
            {
                DrawGrid(e.Graphics, PaintBoard.ClientSize, 50);
            }
        }

        // Function used to initialize the canvas
        private void InitializeCanvas(int width, int height)
        {
            // Dispose of previous images
            paintBoardImage?.Dispose();
            backupImage?.Dispose();

            // Create new bitmap and graphics object
            paintBoardImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            paintBoardImage.SetResolution(144f, 144f);
            graph = Graphics.FromImage(paintBoardImage);

            graph.Clear(Color.White);
            backupImage = (Bitmap)paintBoardImage.Clone();

            // Update PaintBoard control
            PaintBoard.Image = paintBoardImage;
            PaintBoard.Width = paintBoardImage.Width;
            PaintBoard.Height = paintBoardImage.Height;
            PaintBoard.Visible = true;

            // Adjust canvasPanel size and check for transparency
            canvasPanel.AutoScrollMinSize = new Size(paintBoardImage.Width, paintBoardImage.Height);
            isTransparent = ImageHasTransparency(paintBoardImage);
        }

        // ================ Menu ================
        // File Menu Items

        // On choosing new file menu entry
        private void NewFile_Click(object sender, EventArgs e)
        {
            using (NewFileForm newFile = new NewFileForm())
            {
                if (newFile.ShowDialog() == DialogResult.OK)
                {
                    int width = newFile.PageWidth;
                    int height = newFile.PageHeight;
                    InitializeCanvas(width, height);
                }
            }
        }

        // On choosing open file menu entry
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Title = "Open an Image File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                loadImage = new Bitmap(openFileDialog.FileName); // used to bypass Microsoft bug in SetResolution() function
                paintBoardImage = new Bitmap(loadImage);
                paintBoardImage.SetResolution(144f, 144f);

                float xDpi = paintBoardImage.HorizontalResolution;
                float yDpi = paintBoardImage.VerticalResolution;

                backupImage = (Bitmap)paintBoardImage.Clone();
                graph = Graphics.FromImage(paintBoardImage);
                PaintBoard.Image = paintBoardImage;
                PaintBoard.Width = paintBoardImage.Width;
                PaintBoard.Height = paintBoardImage.Height;
                PaintBoard.Visible = true;
                canvasPanel.AutoScrollMinSize = new Size(paintBoardImage.Width, paintBoardImage.Height);
                isTransparent = ImageHasTransparency(paintBoardImage);
                isImageLoaded = true;
            }
        }

        // On choosing save file menu entry
        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPeg Image|*.jpg|PNG Image|*.png";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        this.paintBoardImage.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.paintBoardImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }

                fs.Close();
            }
        }

        // On choosing print file menu entry
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (e.Graphics != null)
                e.Graphics.DrawImage(paintBoardImage, 0, 0);
        }

        private void PrintFile_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        // On choosing exit file menu entry
        private void ExitFile_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Edit Menu Items
        private void FlipVertical_Click(object sender, EventArgs e)
        {
            ApplyImageFlip(RotateFlipType.RotateNoneFlipY);
        }

        private void FlipHorizontal_Click(object sender, EventArgs e)
        {
            ApplyImageFlip(RotateFlipType.RotateNoneFlipX);
        }

        // Function to apply flip to image
        private void ApplyImageFlip(RotateFlipType flipType)
        {
            paintBoardImage.RotateFlip(flipType);
            PaintBoard.Image = paintBoardImage;
            PaintBoard.Refresh();
        }

        private void enableGrid_Click(object sender, EventArgs e) // Enable Grid checkbox
        {
            PaintBoard.Invalidate();
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e) // Resize edit menu entry
        {
            if (!isImageLoaded)
            {
                MessageBox.Show("No image loaded.");
                return;
            }

            using (ResizeForm resizeForm = new ResizeForm(paintBoardImage))
            {
                if (resizeForm.ShowDialog() == DialogResult.OK)
                {
                    // Update paintBoardImage with the resized image
                    paintBoardImage = resizeForm.ResizedImage;
                    PaintBoard.Size = paintBoardImage.Size;
                    PaintBoard.Image = paintBoardImage;
                    graph = Graphics.FromImage(paintBoardImage);
                    PaintBoard.Invalidate(); // Redraw PictureBox with the resized image
                }
            }
        }

        // Filter Menu Items
        // Applies seleted filter and updates the picture in PaintBoard
        private void ApplyFilterAndRefresh(Action<Bitmap> filterAction)
        {
            if (paintBoardImage != null)
            {
                filterAction(paintBoardImage);
                PaintBoard.Invalidate();
            }
        }

        // On choosing channel mixer
        private void ChannelMixerFilter_Click(object sender, EventArgs e)
        {
            ChannelsForm channelsForm = new ChannelsForm(this);
            channelsForm.ShowDialog();
        }

        // On choosing monochrome
        private void MonochromeFilter_Click(object sender, EventArgs e)
        {
            ApplyFilterAndRefresh(filter.ApplyMonochrome);
        }

        // On choosing blur
        private void BlurFilter_Click(object sender, EventArgs e)
        {
            BlurForm blurForm = new BlurForm(this);
            blurForm.ShowDialog();
        }

        // On choosing sharpen
        private void SharpenFilter_Click(object sender, EventArgs e)
        {
            SharpenForm sharpenForm = new SharpenForm(this);
            sharpenForm.ShowDialog();
        }

        // On choosing inverted
        private void InvertedFilter_Click(object sender, EventArgs e)
        {
            ApplyFilterAndRefresh(filter.ApplyInvert);
        }

        // On choosing stylization
        private void StylizationFilter_Click(object sender, EventArgs e)
        {
            SetPaintBoardImage(filter.ApplyStylization(paintBoardImage, 1));
        }

        // On choosing pencil sketch
        private void PencilSketchFilter_Click(object sender, EventArgs e)
        {
            SetPaintBoardImage(filter.ApplyStylization(paintBoardImage, 2));
        }

        // On choosing details enhance
        private void DetailsEnhanceFilter_Click(object sender, EventArgs e)
        {
            SetPaintBoardImage(filter.ApplyStylization(paintBoardImage, 3));
        }

        // About menu entry
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutForm.ShowDialog();
        }

        // ========== Tools Functions ==========
        // Brush
        public void SetBrushColor(Color color) // Sets brush color
        {
            brushColor = color;
            brush.Color = brushColor;

            // Ensure the palette reflects this color change
            palette.UpdateSelectedColor(color);
        }

        public void SetBrushSize(int size) // Sets brush size
        {
            brushSize = size;
            brush.Width = brushSize;
        }

        public void SetBrushShape(string shape) // Sets brush shape
        {
            brushShape = shape;
        }

        private void DrawBrush(MouseEventArgs e) // drawing logics for brush and eraser
        {
            if (tool == 1 || tool == 2)
            {
                int alpha;
                if (isTransparent) alpha = 0;
                else alpha = 255;

                using (GraphicsPath path = new GraphicsPath())
                {
                    Rectangle brushRect = new Rectangle(e.X - brushSize / 2, e.Y - brushSize / 2, brushSize, brushSize);

                    if (brushShape == "Circle")
                    {
                        path.AddEllipse(brushRect);
                    }
                    else if (brushShape == "Rectangle")
                    {
                        path.AddRectangle(brushRect);
                    }

                    if (isDrawing && py != Point.Empty && tool == 1) // Draw a line from the last point to the current point
                    {
                        using (Pen pen = new Pen(brushColor, brushSize))
                        {
                            pen.StartCap = LineCap.Round;
                            pen.EndCap = LineCap.Round;
                            graph.DrawLine(pen, py, e.Location);
                        }
                    }
                    else if (isDrawing && py != Point.Empty && tool == 2) // Eraser mode with transparency
                    {
                        // Set compositing mode to clear (to erase the pixels)
                        graph.CompositingMode = CompositingMode.SourceCopy;

                        using (Pen pen = new Pen(Color.FromArgb(alpha, 255, 255, 255), brushSize)) // Fully transparent
                        {
                            pen.StartCap = LineCap.Round;
                            pen.EndCap = LineCap.Round;
                            graph.DrawLine(pen, py, e.Location);
                        }

                        graph.CompositingMode = CompositingMode.SourceOver; // Reset to default
                    }

                    if (tool == 1) // Drawing mode
                    {
                        graph.FillPath(new SolidBrush(brushColor), path);
                    }
                    else if (tool == 2) // Eraser mode
                    {
                        graph.CompositingMode = CompositingMode.SourceCopy; // Set to erase the pixels
                        graph.FillPath(new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)), path); // Fully transparent
                        graph.CompositingMode = CompositingMode.SourceOver; // Reset to default
                    }

                    py = e.Location; // Update the last point
                }
            }
        }

        // Shapes
        private void UpdateShape(PaintEventArgs e, int x1, int y1, int x2, int y2) // Drawing logics for updating ellipse, rectangle and line 
        {
            Graphics g = e.Graphics;

            if (tool == 7)
            {
                g.DrawEllipse(brush, x1, y1, x2, y2);
            }
            if (tool == 6)
            {
                drawingRectangle = new Rectangle(x1, y1, x2, y2);
                g.DrawRectangle(brush, x1, y1, x2, y2);
            }
            if (tool == 5)
            {
                g.DrawLine(brush, cX, cY, x, y);
            }
        }

        private void DrawShape(int x1, int y1, int x2, int y2) // Draws the finite ellipse, rectangle or line on the loaded image
        {
            if (tool == 7)
            {
                graph.DrawEllipse(brush, x1, y1, x2, y2);
            }
            if (tool == 6)
            {
                graph.DrawRectangle(brush, drawingRectangle);
            }
            if (tool == 5)
            {
                graph.DrawLine(brush, x1, y1, x, y);
            }
        }

        // Crop
        private void UpdateCropRectangle() // Logic for realtime selection tool rectangle update
        {
            int x = Math.Min(startPoint.X, currentPoint.X);
            int y = Math.Min(startPoint.Y, currentPoint.Y);
            int width = Math.Abs(startPoint.X - currentPoint.X);
            int height = Math.Abs(startPoint.Y - currentPoint.Y);

            // Ensure the cropping rectangle does not exceed the boundaries of the PictureBox
            if (x < 0)
            {
                x = 0;
                width = Math.Abs(startPoint.X - 0);
            }
            if (y < 0)
            {
                y = 0;
                height = Math.Abs(startPoint.Y - 0);
            }
            if (paintBoardImage != null)
            {
                if (x + width > paintBoardImage.Width) width = paintBoardImage.Width - x;
                if (y + height > paintBoardImage.Height) height = paintBoardImage.Height - y;

            }

            cropRectangle = new Rectangle(x, y, width, height);
        }

        private Bitmap CropImage(Rectangle rect, int option) // Logics for cropping selected area from the picture
        {
            // Crop the specified area and copy to buffer
            Bitmap croppedImage = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(paintBoardImage, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            // Set the cropped image to the clipboard
            Clipboard.SetImage(croppedImage);

            // Fill the cropped area with white color if chosen "cut"
            if (option == 0)
            {
                using (Graphics g = Graphics.FromImage(paintBoardImage))
                {
                    g.FillRectangle(Brushes.White, rect);
                }

                // Update the PictureBox with the modified image
                PaintBoard.Image = paintBoardImage;
            }

            return croppedImage;
        }

        private void ResetSelection() // Resets selection rectangle
        {
            // Clear the cropping rectangle
            cropRectangle = Rectangle.Empty;
            PaintBoard.Invalidate();
            isSelected = false;
            isDragging = false;
        }

        private void MoveCropRectangle() // Moves Crop rectangle
        {
            int newX = currentPoint.X - offset.X;
            int newY = currentPoint.Y - offset.Y;

            // Ensure the cropping rectangle does not exceed the boundaries of the PictureBox
            if (newX < 0) newX = 0;
            if (newY < 0) newY = 0;
            if (newX + cropRectangle.Width > PaintBoard.Width) newX = PaintBoard.Width - cropRectangle.Width;
            if (newY + cropRectangle.Height > PaintBoard.Height) newY = PaintBoard.Height - cropRectangle.Height;

            cropRectangle = new Rectangle(newX, newY, cropRectangle.Width, cropRectangle.Height);
        }

        public Bitmap CropArea(int option) // Helping function for cropping the area from image
        {
            Bitmap croppedImage = null;

            if (cropRectangle != Rectangle.Empty)
            {
                if (option == 0)
                    croppedImage = CropImage(cropRectangle, 0);
                else
                    croppedImage = CropImage(cropRectangle, 1);

                PaintBoard.Invalidate();  // Refresh the PictureBox to reflect changes
                ResetSelection();
            }
            return croppedImage;
        }

        public void PasteOnImage(Bitmap targetImage, Bitmap extractedImage, Point location) // used for pasting stored in clipboard image data
        {
            extractedImage.SetResolution(targetImage.HorizontalResolution, targetImage.VerticalResolution);
            using (Graphics g = Graphics.FromImage(targetImage))
            {
                g.DrawImage(extractedImage, location);
            }

            PaintBoard.Invalidate(); // Redraw the PaintBoard to reflect the changes
            PaintBoard.Image = paintBoardImage;
            ResetSelection();
        }

        // Text
        private void TextToolTextBox_KeyPress(object sender, KeyPressEventArgs e) // Event for pressing keys in text box used for text tool 
        {
            if (tool == 8 && textPosition != Point.Empty)
            {
                isWriting = true;
                if (e.KeyChar == (char)Keys.Enter)
                {


                    if (IsOutline)
                    {
                        paintBoardImage = ApplyOutlinedTextToImage(paintBoardImage, enteredText, textFont, textPosition, brushColor, outlineColor, outlineSize);
                        PaintBoard.Image = paintBoardImage;
                        graph = Graphics.FromImage(paintBoardImage);
                    }
                    else
                    {
                        using (Graphics g = Graphics.FromImage(paintBoardImage))
                        {
                            g.DrawString(enteredText, textFont, new SolidBrush(brushColor), textPosition);
                        }
                    }


                    textPosition = Point.Empty;
                    PaintBoard.Invalidate(); // Refresh the PictureBox to update the drawn content
                    isWriting = false;
                    e.Handled = true;
                }
                else if (e.KeyChar == (char)Keys.Back)
                {
                    if (enteredText.Length > 0)
                    {
                        enteredText = enteredText.Substring(0, enteredText.Length - 1);
                    }
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    enteredText = string.Empty;
                    textPosition = Point.Empty;
                    PaintBoard.Invalidate(); // Refresh the PictureBox
                    isWriting = false;
                    e.Handled = true;
                }
                else
                {
                    enteredText += e.KeyChar;
                }

                PaintBoard.Invalidate(); // Refresh the PictureBox to update the dashed rectangle and text
            }
        }

        public void PickFont() // Dialog for picking font for text tool
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.Font = textFont; // Set the initial font in the dialog

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    // Store the chosen font and font size
                    textFont = fontDialog.Font;
                }
            }
        }

        // Function for drawing outlined text on the selected image
        public Bitmap ApplyOutlinedTextToImage(Bitmap image, string text, Font font, Point location, Color textColor, Color outlineColor, float outlineThickness)
        {
            Bitmap outputImage = (Bitmap)image.Clone();

            using (Graphics g = Graphics.FromImage(outputImage))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                g.PageUnit = GraphicsUnit.Pixel;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddString(text, font.FontFamily, (int)font.Style, g.DpiY * font.Size / 72, location, StringFormat.GenericDefault);

                    using (Pen outlinePen = new Pen(outlineColor, outlineThickness) { LineJoin = LineJoin.Round })
                    {
                        g.DrawPath(outlinePen, path);
                    }

                    using (SolidBrush textBrush = new SolidBrush(textColor))
                    {
                        g.FillPath(textBrush, path);
                    }
                }
            }

            return outputImage;
        }

        // Stickers
        public void PickSticker() // Window for choosing a sticker
        {
            using (StickerForm stickerForm = new StickerForm())
            {
                if (stickerForm.ShowDialog() == DialogResult.OK && stickerForm.SelectedSticker != null)
                {
                    stickerImage = stickerForm.SelectedSticker;
                    isStickerSelected = true;
                }
            }
        }

        public void UploadSticker() // Window for uploading a sticker
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    stickerImage = Image.FromFile(openFileDialog.FileName);

                    isStickerSelected = true;
                }
            }
        }

        // Function to add the sticker image to the canvas
        private void ApplySticker(Image stickerImage, Point location)
        {
            int fixedWidth = 50;
            int fixedHeight = 50;

            Image resizedSticker = ResizeImage(stickerImage, fixedWidth, fixedHeight);

            using (Graphics g = Graphics.FromImage(paintBoardImage))
            {
                g.DrawImage(resizedSticker, location);
                PaintBoard.Refresh();
            }

            resizedSticker.Dispose();
            PaintBoard.Image = paintBoardImage;
            stickerImage.Dispose();
        }

        // Helper function to resize an image to a fixed size
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }


        // Function checks if image supports transparency
        public bool ImageHasTransparency(Bitmap image)
        {
            // Check if the PixelFormat of the image supports an alpha channel
            return (image.PixelFormat == PixelFormat.Format32bppArgb ||
                    image.PixelFormat == PixelFormat.Format32bppPArgb ||
                    image.PixelFormat == PixelFormat.Format64bppArgb ||
                    image.PixelFormat == PixelFormat.Format64bppPArgb);
        }      

        // Method to draw the grid on the Graphics object
        private void DrawGrid(Graphics g, Size clientSize, int cellSize)
        {
            using (Pen gridPen = new Pen(Color.Gray))
            {
                for (int x = 0; x < clientSize.Width; x += cellSize)
                {
                    g.DrawLine(gridPen, x, 0, x, clientSize.Height);
                }
                for (int y = 0; y < clientSize.Height; y += cellSize)
                {
                    g.DrawLine(gridPen, 0, y, clientSize.Width, y);
                }
            }
        }

    }
}