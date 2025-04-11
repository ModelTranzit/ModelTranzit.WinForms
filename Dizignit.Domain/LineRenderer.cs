using System.Drawing;
using System.Drawing.Imaging;

namespace Dizignit.Domain
{
    public class LineRenderer
    {
        public string FilePath { get; set; }

        public LineRenderer(string filePath)
        {
            FilePath = filePath;
        }


        public Bitmap Render(Bitmap bitmap)
        {
            // Load the image file
            Bitmap lineBitmap = new Bitmap(bitmap);

            // Convert to grayscale for edge detection
            Bitmap grayscaleBitmap = ConvertToGrayscale(lineBitmap);

            // Apply Sobel edge detection
            Bitmap edgeBitmap = ApplySobelFilter(grayscaleBitmap);

            // Save the edge-detected image
            edgeBitmap.Save(FilePath, ImageFormat.Bmp);
            Console.WriteLine("Edge-detected image saved as edges.bmp");

            return edgeBitmap;
        }

        // Convert a bitmap to grayscale
        static Bitmap ConvertToGrayscale(Bitmap bitmap)
        {
            Bitmap grayscaleBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color originalColor = bitmap.GetPixel(x, y);
                    int grayScale = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);
                    Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                    grayscaleBitmap.SetPixel(x, y, grayColor);
                }
            }

            return grayscaleBitmap;
        }

        // Apply Sobel filter for edge detection
        static Bitmap ApplySobelFilter(Bitmap bitmap)
        {
            Bitmap edgeBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            int[,] gx = new int[3, 3] // Sobel X kernel
            {
            {-1, 0, 1},
            {-2, 0, 2},
            {-1, 0, 1}
            };

            int[,] gy = new int[3, 3] // Sobel Y kernel
            {
            {1, 2, 1},
            {0, 0, 0},
            {-1, -2, -1}
            };

            for (int y = 1; y < bitmap.Height - 1; y++)
            {
                for (int x = 1; x < bitmap.Width - 1; x++)
                {
                    int gradientX = 0;
                    int gradientY = 0;

                    // Apply Sobel kernels
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int pixelVal = bitmap.GetPixel(x + kx, y + ky).R; // Use grayscale value
                            gradientX += pixelVal * gx[ky + 1, kx + 1];
                            gradientY += pixelVal * gy[ky + 1, kx + 1];
                        }
                    }

                    // Calculate gradient magnitude
                    int edgeVal = (int)Math.Sqrt((gradientX * gradientX) + (gradientY * gradientY));
                    edgeVal = Math.Min(255, edgeVal); // Clamp to 255

                    // Set edge pixel value
                    edgeBitmap.SetPixel(x, y, Color.FromArgb(edgeVal, edgeVal, edgeVal));
                }
            }

            return edgeBitmap;
        }

    }
}
