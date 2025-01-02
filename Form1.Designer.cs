namespace PixelMaster
{
    partial class PaintForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintForm));
            menuPaint = new MenuStrip();
            fileToolStripMenu = new ToolStripMenuItem();
            newFile = new ToolStripMenuItem();
            openFile = new ToolStripMenuItem();
            saveFile = new ToolStripMenuItem();
            printFile = new ToolStripMenuItem();
            exitFile = new ToolStripMenuItem();
            editToolStripMenu = new ToolStripMenuItem();
            flipVertical = new ToolStripMenuItem();
            flipHorizontal = new ToolStripMenuItem();
            enableGrid = new ToolStripMenuItem();
            resizeToolStripMenuItem = new ToolStripMenuItem();
            filtersToolStripMenu = new ToolStripMenuItem();
            channelMixer = new ToolStripMenuItem();
            monochromeFilter = new ToolStripMenuItem();
            blurFilter = new ToolStripMenuItem();
            sharpenFilter = new ToolStripMenuItem();
            invertedFilter = new ToolStripMenuItem();
            stylizationFilter = new ToolStripMenuItem();
            pencilSketchFilter = new ToolStripMenuItem();
            detailsEnhanceFilter = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            colorDialog1 = new ColorDialog();
            statusStrip1 = new StatusStrip();
            coordinatesLabel = new ToolStripStatusLabel();
            panel2 = new Panel();
            textToolTextBox = new TextBox();
            PaintBoard = new PictureBox();
            toolStrip1 = new ToolStrip();
            btnBrush = new ToolStripButton();
            btnEraser = new ToolStripButton();
            btnFill = new ToolStripButton();
            btnColorPicker = new ToolStripButton();
            btnDrawLine = new ToolStripButton();
            btnDrawRectangle = new ToolStripButton();
            btnDrawEllipse = new ToolStripButton();
            btnSelect = new ToolStripButton();
            textDrawer = new ToolStripButton();
            btnClearBoard = new ToolStripButton();
            btnMagicWand = new ToolStripButton();
            btnStickers = new ToolStripButton();
            panel1 = new Panel();
            canvasPanel = new Panel();
            menuPaint.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PaintBoard).BeginInit();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            canvasPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuPaint
            // 
            menuPaint.BackColor = SystemColors.Control;
            menuPaint.ImageScalingSize = new Size(24, 24);
            menuPaint.Items.AddRange(new ToolStripItem[] { fileToolStripMenu, editToolStripMenu, filtersToolStripMenu, aboutToolStripMenuItem });
            menuPaint.Location = new Point(0, 0);
            menuPaint.Name = "menuPaint";
            menuPaint.Padding = new Padding(9, 3, 0, 3);
            menuPaint.Size = new Size(2070, 35);
            menuPaint.TabIndex = 2;
            menuPaint.Text = "Menu";
            // 
            // fileToolStripMenu
            // 
            fileToolStripMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, openFile, saveFile, printFile, exitFile });
            fileToolStripMenu.Name = "fileToolStripMenu";
            fileToolStripMenu.Size = new Size(54, 29);
            fileToolStripMenu.Text = "File";
            // 
            // newFile
            // 
            newFile.Name = "newFile";
            newFile.Size = new Size(158, 34);
            newFile.Text = "New";
            newFile.Click += NewFile_Click;
            // 
            // openFile
            // 
            openFile.Name = "openFile";
            openFile.Size = new Size(158, 34);
            openFile.Text = "Open";
            openFile.Click += OpenFile_Click;
            // 
            // saveFile
            // 
            saveFile.Name = "saveFile";
            saveFile.Size = new Size(158, 34);
            saveFile.Text = "Save";
            saveFile.Click += SaveFile_Click;
            // 
            // printFile
            // 
            printFile.Name = "printFile";
            printFile.Size = new Size(158, 34);
            printFile.Text = "Print";
            printFile.Click += PrintFile_Click;
            // 
            // exitFile
            // 
            exitFile.Name = "exitFile";
            exitFile.Size = new Size(158, 34);
            exitFile.Text = "Exit";
            exitFile.Click += ExitFile_Click;
            // 
            // editToolStripMenu
            // 
            editToolStripMenu.DropDownItems.AddRange(new ToolStripItem[] { flipVertical, flipHorizontal, enableGrid, resizeToolStripMenuItem });
            editToolStripMenu.Name = "editToolStripMenu";
            editToolStripMenu.Size = new Size(58, 29);
            editToolStripMenu.Text = "Edit";
            // 
            // flipVertical
            // 
            flipVertical.Name = "flipVertical";
            flipVertical.Size = new Size(270, 34);
            flipVertical.Text = "Flip Vertical";
            flipVertical.Click += FlipVertical_Click;
            // 
            // flipHorizontal
            // 
            flipHorizontal.Name = "flipHorizontal";
            flipHorizontal.Size = new Size(270, 34);
            flipHorizontal.Text = "Flip Horizontal";
            flipHorizontal.Click += FlipHorizontal_Click;
            // 
            // enableGrid
            // 
            enableGrid.CheckOnClick = true;
            enableGrid.Name = "enableGrid";
            enableGrid.Size = new Size(270, 34);
            enableGrid.Text = "Grid";
            enableGrid.Click += enableGrid_Click;
            // 
            // resizeToolStripMenuItem
            // 
            resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            resizeToolStripMenuItem.Size = new Size(270, 34);
            resizeToolStripMenuItem.Text = "Resize";
            resizeToolStripMenuItem.Click += resizeToolStripMenuItem_Click;
            // 
            // filtersToolStripMenu
            // 
            filtersToolStripMenu.DropDownItems.AddRange(new ToolStripItem[] { channelMixer, monochromeFilter, blurFilter, sharpenFilter, invertedFilter, stylizationFilter, pencilSketchFilter, detailsEnhanceFilter });
            filtersToolStripMenu.Name = "filtersToolStripMenu";
            filtersToolStripMenu.Size = new Size(74, 29);
            filtersToolStripMenu.Text = "Filters";
            // 
            // channelMixer
            // 
            channelMixer.Name = "channelMixer";
            channelMixer.Size = new Size(270, 34);
            channelMixer.Text = "Channel Mixer";
            channelMixer.Click += ChannelMixerFilter_Click;
            // 
            // monochromeFilter
            // 
            monochromeFilter.Name = "monochromeFilter";
            monochromeFilter.Size = new Size(270, 34);
            monochromeFilter.Text = "Monochrome";
            monochromeFilter.Click += MonochromeFilter_Click;
            // 
            // blurFilter
            // 
            blurFilter.Name = "blurFilter";
            blurFilter.Size = new Size(270, 34);
            blurFilter.Text = "Blur";
            blurFilter.Click += BlurFilter_Click;
            // 
            // sharpenFilter
            // 
            sharpenFilter.Name = "sharpenFilter";
            sharpenFilter.Size = new Size(270, 34);
            sharpenFilter.Text = "Sharpen";
            sharpenFilter.Click += SharpenFilter_Click;
            // 
            // invertedFilter
            // 
            invertedFilter.Name = "invertedFilter";
            invertedFilter.Size = new Size(270, 34);
            invertedFilter.Text = "Inverted";
            invertedFilter.Click += InvertedFilter_Click;
            // 
            // stylizationFilter
            // 
            stylizationFilter.Name = "stylizationFilter";
            stylizationFilter.Size = new Size(270, 34);
            stylizationFilter.Text = "Stylization";
            stylizationFilter.Click += StylizationFilter_Click;
            // 
            // pencilSketchFilter
            // 
            pencilSketchFilter.Name = "pencilSketchFilter";
            pencilSketchFilter.Size = new Size(270, 34);
            pencilSketchFilter.Text = "Pencil Sketch";
            pencilSketchFilter.Click += PencilSketchFilter_Click;
            // 
            // detailsEnhanceFilter
            // 
            detailsEnhanceFilter.Name = "detailsEnhanceFilter";
            detailsEnhanceFilter.Size = new Size(270, 34);
            detailsEnhanceFilter.Text = "Details Enhance";
            detailsEnhanceFilter.Click += DetailsEnhanceFilter_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(78, 29);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { coordinatesLabel });
            statusStrip1.Location = new Point(0, 1400);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(2070, 22);
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // coordinatesLabel
            // 
            coordinatesLabel.Name = "coordinatesLabel";
            coordinatesLabel.Size = new Size(0, 15);
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 35);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(126, 0, 126, 0);
            panel2.Size = new Size(2070, 80);
            panel2.TabIndex = 10;
            // 
            // textToolTextBox
            // 
            textToolTextBox.Location = new Point(-30, 500);
            textToolTextBox.Name = "textToolTextBox";
            textToolTextBox.Size = new Size(10, 31);
            textToolTextBox.TabIndex = 5;
            textToolTextBox.KeyPress += TextToolTextBox_KeyPress;
            // 
            // PaintBoard
            // 
            PaintBoard.BackColor = SystemColors.Control;
            PaintBoard.BackgroundImage = Properties.Resources.grid;
            PaintBoard.ErrorImage = null;
            PaintBoard.Location = new Point(0, 0);
            PaintBoard.Margin = new Padding(4, 5, 4, 5);
            PaintBoard.Name = "PaintBoard";
            PaintBoard.Size = new Size(749, 540);
            PaintBoard.TabIndex = 0;
            PaintBoard.TabStop = false;
            PaintBoard.Visible = false;
            PaintBoard.Paint += PaintBoard_Paint;
            PaintBoard.MouseClick += PaintBoard_MouseClick;
            PaintBoard.MouseDown += PaintBoard_MouseDown;
            PaintBoard.MouseMove += PaintBoard_MouseMove;
            PaintBoard.MouseUp += PaintBoard_MouseUp;
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.BackColor = SystemColors.ControlLight;
            toolStrip1.Dock = DockStyle.Right;
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBrush, btnEraser, btnFill, btnColorPicker, btnDrawLine, btnDrawRectangle, btnDrawEllipse, btnSelect, textDrawer, btnClearBoard, btnMagicWand, btnStickers });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
            toolStrip1.Location = new Point(2010, 115);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 3, 0);
            toolStrip1.Size = new Size(60, 1285);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnBrush
            // 
            btnBrush.AutoSize = false;
            btnBrush.CheckOnClick = true;
            btnBrush.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBrush.Image = Properties.Resources.brush;
            btnBrush.ImageTransparentColor = Color.Magenta;
            btnBrush.Name = "btnBrush";
            btnBrush.Size = new Size(32, 32);
            btnBrush.Text = "1";
            btnBrush.Click += BtnBrush_Click;
            // 
            // btnEraser
            // 
            btnEraser.AutoSize = false;
            btnEraser.CheckOnClick = true;
            btnEraser.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEraser.Image = Properties.Resources.eraser;
            btnEraser.ImageTransparentColor = Color.Magenta;
            btnEraser.Name = "btnEraser";
            btnEraser.Size = new Size(32, 32);
            btnEraser.Text = "2";
            btnEraser.Click += BtnEraser_Click;
            // 
            // btnFill
            // 
            btnFill.AutoSize = false;
            btnFill.CheckOnClick = true;
            btnFill.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFill.Image = Properties.Resources.fill;
            btnFill.ImageTransparentColor = Color.Magenta;
            btnFill.Name = "btnFill";
            btnFill.Size = new Size(32, 32);
            btnFill.Text = "3";
            btnFill.Click += BtnFill_Click;
            // 
            // btnColorPicker
            // 
            btnColorPicker.AutoSize = false;
            btnColorPicker.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnColorPicker.Image = (Image)resources.GetObject("btnColorPicker.Image");
            btnColorPicker.ImageTransparentColor = Color.Magenta;
            btnColorPicker.Name = "btnColorPicker";
            btnColorPicker.Size = new Size(32, 32);
            btnColorPicker.Text = "4";
            btnColorPicker.Click += BtnColorPicker_Click;
            // 
            // btnDrawLine
            // 
            btnDrawLine.AutoSize = false;
            btnDrawLine.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDrawLine.Image = Properties.Resources.diagonal_line;
            btnDrawLine.ImageTransparentColor = Color.Magenta;
            btnDrawLine.Name = "btnDrawLine";
            btnDrawLine.Size = new Size(32, 32);
            btnDrawLine.Text = "5";
            btnDrawLine.Click += BtnDrawLine_Click;
            // 
            // btnDrawRectangle
            // 
            btnDrawRectangle.AutoSize = false;
            btnDrawRectangle.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDrawRectangle.Image = Properties.Resources.unfilled_square;
            btnDrawRectangle.ImageTransparentColor = Color.Magenta;
            btnDrawRectangle.Name = "btnDrawRectangle";
            btnDrawRectangle.Size = new Size(32, 32);
            btnDrawRectangle.Text = "6";
            btnDrawRectangle.Click += BtnDrawRectangle_Click;
            // 
            // btnDrawEllipse
            // 
            btnDrawEllipse.AutoSize = false;
            btnDrawEllipse.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDrawEllipse.Image = Properties.Resources.circle;
            btnDrawEllipse.ImageTransparentColor = Color.Magenta;
            btnDrawEllipse.Name = "btnDrawEllipse";
            btnDrawEllipse.Size = new Size(32, 32);
            btnDrawEllipse.Text = "7";
            btnDrawEllipse.Click += BtnDrawEllipse_Click;
            // 
            // btnSelect
            // 
            btnSelect.AutoSize = false;
            btnSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSelect.Image = Properties.Resources.selection;
            btnSelect.ImageTransparentColor = Color.Magenta;
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(32, 32);
            btnSelect.Text = "Ctrl + X";
            btnSelect.Click += BtnSelect_Click;
            // 
            // textDrawer
            // 
            textDrawer.AutoSize = false;
            textDrawer.DisplayStyle = ToolStripItemDisplayStyle.Image;
            textDrawer.Image = Properties.Resources.text;
            textDrawer.ImageTransparentColor = Color.Magenta;
            textDrawer.Name = "textDrawer";
            textDrawer.Size = new Size(32, 32);
            textDrawer.Text = "T";
            textDrawer.Click += TextDrawer_Click;
            // 
            // btnClearBoard
            // 
            btnClearBoard.AutoSize = false;
            btnClearBoard.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnClearBoard.Image = Properties.Resources.broom;
            btnClearBoard.ImageTransparentColor = Color.Magenta;
            btnClearBoard.Name = "btnClearBoard";
            btnClearBoard.Size = new Size(32, 32);
            btnClearBoard.Text = "Ctrl + D";
            btnClearBoard.Click += BtnClearBoard_Click;
            // 
            // btnMagicWand
            // 
            btnMagicWand.AutoSize = false;
            btnMagicWand.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnMagicWand.Image = Properties.Resources.magic_wand;
            btnMagicWand.ImageTransparentColor = Color.Magenta;
            btnMagicWand.Name = "btnMagicWand";
            btnMagicWand.Size = new Size(32, 32);
            btnMagicWand.Text = "M";
            btnMagicWand.Click += btnMagicWand_Click;
            // 
            // btnStickers
            // 
            btnStickers.AutoSize = false;
            btnStickers.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnStickers.Image = Properties.Resources.stickerpng;
            btnStickers.ImageTransparentColor = Color.Magenta;
            btnStickers.Name = "btnStickers";
            btnStickers.Size = new Size(32, 32);
            btnStickers.Text = "S";
            btnStickers.Click += BtnStickers_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(textToolTextBox);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 115);
            panel1.Name = "panel1";
            panel1.Size = new Size(143, 1285);
            panel1.TabIndex = 9;
            // 
            // canvasPanel
            // 
            canvasPanel.BackColor = SystemColors.GradientInactiveCaption;
            canvasPanel.Controls.Add(PaintBoard);
            canvasPanel.Dock = DockStyle.Fill;
            canvasPanel.Location = new Point(143, 115);
            canvasPanel.Name = "canvasPanel";
            canvasPanel.Size = new Size(1867, 1285);
            canvasPanel.TabIndex = 12;
            // 
            // PaintForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2070, 1422);
            Controls.Add(canvasPanel);
            Controls.Add(toolStrip1);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(statusStrip1);
            Controls.Add(menuPaint);
            KeyPreview = true;
            MainMenuStrip = menuPaint;
            Margin = new Padding(4, 5, 4, 5);
            Name = "PaintForm";
            Text = "PixelMaster";
            menuPaint.ResumeLayout(false);
            menuPaint.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PaintBoard).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            canvasPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuPaint;
        private ToolStripMenuItem fileToolStripMenu;
        private ToolStripMenuItem saveFile;
        private ToolStripMenuItem openFile;
        private ColorDialog colorDialog1;
        private ToolStripMenuItem editToolStripMenu;
        private ToolStripMenuItem flipVertical;
        private ToolStripMenuItem flipHorizontal;
        private ToolStripMenuItem exitFile;
        private ToolStripMenuItem printFile;
        private ToolStripMenuItem filtersToolStripMenu;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel coordinatesLabel;
        private Panel panel2;
        private PictureBox PaintBoard;
        private ToolStrip toolStrip1;
        private ToolStripButton btnBrush;
        private ToolStripButton btnEraser;
        private ToolStripButton btnFill;
        private ToolStripButton btnDrawLine;
        private ToolStripButton btnDrawRectangle;
        private ToolStripButton btnDrawEllipse;
        private ToolStripButton textDrawer;
        private ToolStripButton btnClearBoard;
        private ToolStripButton btnSelect;
        private Panel panel1;
        private Panel canvasPanel;
        private ToolStripMenuItem monochromeFilter;
        private ToolStripMenuItem blurFilter;
        private ToolStripMenuItem invertedFilter;
        private ToolStripMenuItem channelMixer;
        private ToolStripButton btnColorPicker;
        private TextBox textToolTextBox;
        private ToolStripMenuItem sharpenFilter;
        private ToolStripMenuItem stylizationFilter;
        private ToolStripMenuItem pencilSketchFilter;
        private ToolStripMenuItem detailsEnhanceFilter;
        private ToolStripMenuItem newFile;
        private ToolStripButton btnMagicWand;
        private ToolStripButton btnStickers;
        private ToolStripMenuItem enableGrid;
        private ToolStripMenuItem resizeToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}
