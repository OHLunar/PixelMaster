using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Numerics;

namespace PixelMaster
{
    internal class ShapeFiller // Class for flood fill logic
    {
        // Applies a flood fill operation to the input bitmap based on a selected point and threshold.
        // The function also extracts the filled path points for visualization or further processing.
        public Bitmap FloodFill(Bitmap inputBitmap, System.Drawing.Point clickPoint, int threshold, out List<System.Drawing.Point> pathPoints, Color color)
        {
            pathPoints = new List<System.Drawing.Point>();
            Scalar value = new Scalar(color.B, color.G, color.R, 255);

            // Convert the input Bitmap to a Mat format, separate color channels, and create a 3-channel Mat for flood fill
            Mat image = BitmapConverter.ToMat(inputBitmap);
            Mat[] channels;
            Cv2.Split(image, out channels);
            Mat bgr = new Mat();
            Cv2.Merge(new[] { channels[0], channels[1], channels[2] }, bgr);

            // Prepare a mask for flood fill, offsetting by 2 pixels around the edges to fit OpenCV requirements
            Mat mask = new Mat(bgr.Rows + 2, bgr.Cols + 2, MatType.CV_8UC1, Scalar.All(0));
            OpenCvSharp.Point seedPoint = new OpenCvSharp.Point(clickPoint.X, clickPoint.Y);

            // Execute flood fill with defined color difference thresholds
            Scalar lowerDiff = new Scalar(threshold, threshold, threshold);
            Scalar upperDiff = new Scalar(threshold, threshold, threshold);
            Rect floodFilledRegion;
            Cv2.FloodFill(bgr, mask, seedPoint, Scalar.White, out floodFilledRegion, lowerDiff, upperDiff);

            // Extract filled points from the mask and adjust to match original image coordinates
            for (int y = 2; y < mask.Rows - 2; y++)
            {
                for (int x = 2; x < mask.Cols - 2; x++)
                {
                    if (mask.At<byte>(y, x) > 0)
                    {
                        pathPoints.Add(new System.Drawing.Point(x - 1, y - 1));
                    }
                }
            }

            // Merge channels back into a single Mat, overlaying the filled points as dots, then convert to Bitmap
            Mat output = new Mat();
            Cv2.Merge(new[] { channels[0], channels[1], channels[2], channels[3] }, output);
            foreach (var point in pathPoints)
            {
                Cv2.Circle(output, new OpenCvSharp.Point(point.X, point.Y), 1, value, -1, LineTypes.AntiAlias);
            }
            return BitmapConverter.ToBitmap(output);
        }
    }
}
