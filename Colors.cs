using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PixelMaster
{
    internal class Colors // Class for color and channel related operations
    {
        Mat img;

        public Colors() 
        {
            img = new Mat(); // Initializing img variable
        }

        public void LoadMat(Bitmap bitmap) // loads provided parameter bitmap as OpenCV mat file
        {
            img = BitmapConverter.ToMat(bitmap);
        }

        // sets mat image as bitmap for paintBoardImage in main form
        public void SetBitmap(Mat image, PaintForm paintForm) 
        {
            Bitmap bitmap = BitmapConverter.ToBitmap(image);
            paintForm.paintBoardImage = bitmap;
        }

        // Scales value from slider into byte value to use in channel splitting operations
        public byte ScaleByteValue(byte originalValue, int sliderValue) 
        {
            int sliderMax = 100;
            // Calculate the scaled value
            double scaleFactor = (double)sliderValue / sliderMax;
            return (byte)(originalValue * scaleFactor);
        }

        // Sets value of RGB to parameter bitmap image and returns modified image as bitmap
        public Bitmap ChangeChannels(Bitmap image, int blue, int green, int red) 
        {
            Mat mat = BitmapConverter.ToMat(image);
            var mat3 = new Mat<Vec3b>(mat); // 3d byte vector type
            var indexer = mat3.GetIndexer();

            for (int y = 0; y < mat.Height; y++)
            {
                for (int x = 0; x < mat.Width; x++)
                {
                    Vec3b color = indexer[y, x];
                    color.Item0 = ScaleByteValue(color.Item0, blue); // Blue
                    color.Item1 = ScaleByteValue(color.Item1, green); // Green
                    color.Item2 = ScaleByteValue(color.Item2, red); // Red
                    indexer[y, x] = color;
                }
            }

            return BitmapConverter.ToBitmap(mat);
        }

        // Obsolete FloodFill function, is not used in program but can be used instead actual one
        public Bitmap FloodFill(Bitmap bitmap, System.Drawing.Point point, Color color) 
        {
            OpenCvSharp.Point cvpoint = new OpenCvSharp.Point(point.X, point.Y);
            Mat image = BitmapConverter.ToMat(bitmap);
            Scalar value = new Scalar(color.B, color.G, color.R);
            Cv2.FloodFill(image, cvpoint, value);

            return BitmapConverter.ToBitmap(image);
        }

    }
}
