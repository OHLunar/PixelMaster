using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PixelMaster
{
    internal class Filter // Class for filters logic
    {
        
        public void ApplyMonochrome(Bitmap image) // Applies a Monochrome (black and white) filter to the image
        {
            int width = image.Width;
            int height = image.Height;

            // Lock the bitmap's bits for efficient processing
            BitmapData bitmapData = image.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int bytesPerPixel = 3;
            int stride = bitmapData.Stride;
            int byteCount = stride * height;
            byte[] pixels = new byte[byteCount];

            // Copy the bitmap's pixel data into the array
            Marshal.Copy(bitmapData.Scan0, pixels, 0, byteCount);

            for (int y = 0; y < height; y++)
            {
                int rowOffset = y * stride;

                for (int x = 0; x < width; x++)
                {
                    int pixelOffset = rowOffset + x * bytesPerPixel;

                    // Extract the color components
                    byte blue = pixels[pixelOffset];
                    byte green = pixels[pixelOffset + 1];
                    byte red = pixels[pixelOffset + 2];

                    // Calculate the grayscale value
                    byte gray = (byte)((red * 0.3) + (green * 0.59) + (blue * 0.11));

                    // Set the grayscale value
                    pixels[pixelOffset] = gray;
                    pixels[pixelOffset + 1] = gray;
                    pixels[pixelOffset + 2] = gray;
                }
            }

            // Copy the modified pixel data back into the bitmap
            Marshal.Copy(pixels, 0, bitmapData.Scan0, byteCount);

            // Unlock the bits
            image.UnlockBits(bitmapData);
        }

        public Bitmap SharpenImage(Bitmap image, float scale) // Applies Sharpen Image filter with provided intensity
        {
            // Convert the input bitmap to a Mat
            Mat matImage = BitmapConverter.ToMat(image);

            // Define the sharpening kernel as a float array
            float[,] kernelArray = {
                { -1f, -1f, -1f },
                { -1f,  scale, -1f },
                { -1f, -1f, -1f }
            };

            // Create a Mat object to hold the kernel
            Mat kernel = new Mat(3, 3, MatType.CV_32F);

            // Set the values in the kernel Mat
            for (int i = 0; i < kernel.Rows; i++)
            {
                for (int j = 0; j < kernel.Cols; j++)
                {
                    kernel.Set<float>(i, j, kernelArray[i, j]);
                }
            }

            // Apply the sharpening kernel
            Mat sharpened = new Mat();
            Cv2.Filter2D(matImage, sharpened, -1, kernel);

            // Convert the result back to a Bitmap and return
            return BitmapConverter.ToBitmap(sharpened);
        }

        public Bitmap ApplyBlur(Bitmap image, int blurSize) // Applies Blur filter with provided intensity
        {
            Bitmap blurredImage = (Bitmap)image.Clone(); // Clone the original image to avoid modifying it directly
            Bitmap temp = (Bitmap)blurredImage.Clone(); // Create a temporary bitmap for processing

            int width = image.Width;
            int height = image.Height;

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = blurredImage.LockBits(rect, ImageLockMode.ReadWrite, image.PixelFormat);
            BitmapData tempData = temp.LockBits(rect, ImageLockMode.ReadOnly, image.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(image.PixelFormat) / 8;
            int stride = bmpData.Stride;
            int byteCount = stride * height;
            byte[] pixels = new byte[byteCount];
            byte[] tempPixels = new byte[byteCount];

            Marshal.Copy(bmpData.Scan0, pixels, 0, byteCount);
            Marshal.Copy(tempData.Scan0, tempPixels, 0, byteCount);

            // Horizontal blur pass
            for (int y = 0; y < height; y++)
            {
                int yOffset = y * stride;
                for (int x = blurSize; x < width - blurSize; x++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;

                    for (int kx = -blurSize; kx <= blurSize; kx++)
                    {
                        int px = yOffset + (x + kx) * bytesPerPixel;
                        avgR += tempPixels[px + 2]; // Red
                        avgG += tempPixels[px + 1]; // Green
                        avgB += tempPixels[px];     // Blue
                    }

                    int blurPixelCount = blurSize * 2 + 1;

                    avgR /= blurPixelCount;
                    avgG /= blurPixelCount;
                    avgB /= blurPixelCount;

                    int position = yOffset + x * bytesPerPixel;
                    pixels[position] = (byte)avgB;       // Blue
                    pixels[position + 1] = (byte)avgG;   // Green
                    pixels[position + 2] = (byte)avgR;   // Red
                }
            }

            // Copy the horizontally blurred pixels back to the temp array for the vertical pass
            Marshal.Copy(pixels, 0, tempData.Scan0, byteCount);

            // Vertical blur pass
            for (int x = 0; x < width; x++)
            {
                for (int y = blurSize; y < height - blurSize; y++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;

                    for (int ky = -blurSize; ky <= blurSize; ky++)
                    {
                        int px = (y + ky) * stride + x * bytesPerPixel;
                        avgR += tempPixels[px + 2]; // Red
                        avgG += tempPixels[px + 1]; // Green
                        avgB += tempPixels[px];     // Blue
                    }

                    int blurPixelCount = blurSize * 2 + 1;

                    avgR /= blurPixelCount;
                    avgG /= blurPixelCount;
                    avgB /= blurPixelCount;

                    int position = y * stride + x * bytesPerPixel;
                    pixels[position] = (byte)avgB;       // Blue
                    pixels[position + 1] = (byte)avgG;   // Green
                    pixels[position + 2] = (byte)avgR;   // Red
                }
            }

            Marshal.Copy(pixels, 0, bmpData.Scan0, byteCount);
            blurredImage.UnlockBits(bmpData);
            temp.UnlockBits(tempData);
            temp.Dispose();

            return blurredImage; // Return the blurred image
        }
        
        public void ApplyInvert(Bitmap image) // Applies an invert colors filter to the image
        {
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    Color invertedColor = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);
                    image.SetPixel(x, y, invertedColor);
                }
            }
        }

        public Bitmap ApplyStylization(Bitmap image, int type) // Applies stylization filters depending on type sent as parameter
        {
            Mat source = BitmapConverter.ToMat(image); // loading image into Mat type
            Mat[] channels = new Mat[4];
            Cv2.Split(source, out channels); // Splitting image into 4 channels

            Mat gbr = new Mat(), gbrOut = new Mat(), dst1 = new Mat();
            Mat[] gbrChannels = { channels[0], channels[1], channels[2] };
            Cv2.Merge(gbrChannels, gbr); // creating 3-channel image (no transparency)

            if (type == 1)
                Cv2.Stylization(gbr, gbrOut);
            else if (type == 2)
                Cv2.PencilSketch(gbr, dst1, gbrOut);
            else if (type == 3)
                Cv2.DetailEnhance(gbr, gbrOut);

            Cv2.Split(gbrOut, out gbrChannels); // Splitting modified image into channels
            Mat[] mergedChannels = { gbrChannels[0], gbrChannels[1], gbrChannels[2], channels[3] }; // Creating Mat[] array with all 4 channels
            Mat dest = new Mat();
            Cv2.Merge(mergedChannels, dest); // Merging all 4 channels into single image

            return BitmapConverter.ToBitmap(dest);
        }
    }
}
