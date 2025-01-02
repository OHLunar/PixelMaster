using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Drawing2D;

namespace PixelMaster
{
    internal class MagicWandTool // Class for magic wand tool
    {
        // Function used for creating the mask and then filling it with transparent pixels
        public Bitmap CreateFilledMask(Bitmap inputBitmap, System.Drawing.Point clickPoint, int threshold, out List<System.Drawing.Point> pathPoints) 
        {
            pathPoints = new List<System.Drawing.Point>();

            // Convert the input Bitmap to a 4-channel Mat (BGRA format)
            Mat image = BitmapConverter.ToMat(inputBitmap);

            // Split the 4 channels (Blue, Green, Red, Alpha)
            Mat[] channels;
            Cv2.Split(image, out channels);

            // Combine the Blue, Green, and Red channels into a 3-channel Mat
            Mat bgr = new Mat();
            Cv2.Merge(new[] { channels[0], channels[1], channels[2] }, bgr);

            // Create a mask, which must be 2 pixels larger in both dimensions than the image
            Mat mask = new Mat(bgr.Rows + 2, bgr.Cols + 2, MatType.CV_8UC1, Scalar.All(0));

            // Set the seed point for FloodFill using the clickPoint
            OpenCvSharp.Point seedPoint = new OpenCvSharp.Point(clickPoint.X, clickPoint.Y);

            // Define the lower and upper color bounds based on the threshold
            Scalar lowerDiff = new Scalar(threshold, threshold, threshold);
            Scalar upperDiff = new Scalar(threshold, threshold, threshold);

            // Apply the FloodFill to the 3-channel image using the mask
            Rect floodFilledRegion;
            Cv2.FloodFill(bgr, mask, seedPoint, Scalar.White, out floodFilledRegion, lowerDiff, upperDiff);

            // Extract the dotted path points
            for (int y = 0; y < mask.Rows; y++)
            {
                for (int x = 0; x < mask.Cols; x++)
                {
                    if (mask.At<byte>(y, x) > 0)
                    {
                        pathPoints.Add(new System.Drawing.Point(x - 1, y - 1)); // Adjust for mask offset
                    }
                }
            }

            // Convert the mask back to the input bitmap dimensions for visualization
            Mat output = new Mat();
            Cv2.Merge(new[] { channels[0], channels[1], channels[2], channels[3] }, output);

            // Fill with transparent
            foreach (var point in pathPoints)
            {
                Cv2.Circle(output, new OpenCvSharp.Point(point.X, point.Y), 1, new Scalar(0, 0, 0, 0), -1, LineTypes.AntiAlias);
            }

            // Convert back to Bitmap
            return BitmapConverter.ToBitmap(output);
        }
    }
}
