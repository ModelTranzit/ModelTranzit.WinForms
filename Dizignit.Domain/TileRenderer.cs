using Dizignit.Core;
using Dizignit.Core.Helpers;
using Dizignit.DAL;
using System.Drawing;

namespace Dizignit.Domain
{
    public class TileRenderer
    {
        private HttpApiRequest _httpApiRequest { get; set; }
        private string _requestID { get; set; }

        public TileRenderer(string requestID)
        {
            _requestID = requestID;
        }

        public byte[] ConverTileToBlack(byte[] originalPixels, MapBitDepth bitDebth)
        {
            var originalBitmat = BitmapHelper.ByteArrayToBitmap(originalPixels);

            // this is going to be the new rail bitmap
            using (Bitmap railBits = new Bitmap(originalBitmat.Width, originalBitmat.Height))
            {
                // Loop through every pixel in the image
                for (int y = 0; y < originalBitmat.Height; y++)
                {
                    for (int x = 0; x < originalBitmat.Width; x++)
                    {
                        var color = originalBitmat.GetPixel(x, y);
                        var hexValue = ColorToHex(color).ToUpper();

                        // now we need to know if the color is one of the rail colors
                        // if it is, we need to change it to make the color black
                        // if not then the pixel should be white

                        var railColors = new List<string>
                        {
                            "#e9eaef".ToUpper(),
                            "#c3c7cb".ToUpper(),
                            "#e6e6e6".ToUpper(),
                            "#d9d9e3".ToUpper(),
                            "#cfd2d5".ToUpper(),
                            "#d9dcdc".ToUpper(),
                            "#e3e6e6".ToUpper(),
                            "#cccfd2".ToUpper(),
                            "#cfd2d5".ToUpper(),
                            "#dddfe6".ToUpper(),
                            //"#".ToUpper()
                        };

                        // if rail color exists then draw it 
                        if (railColors.Contains(hexValue))
                        {
                            // Change the pixel color to black
                            railBits.SetPixel(x, y, Color.Black);
                        }
                        else
                        {
                            // Change the pixel color to white
                            railBits.SetPixel(x, y, Color.White);
                        }
                    }
                }

                return BitmapHelper.BitmapToByteArray(railBits);
            }
        }




















        private static List<Point> _detectEdges(Bitmap bitmap)
        {
            List<Point> edgePoints = new List<Point>();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.ToArgb() == Color.Black.ToArgb()) // Detect black pixels
                    {
                        edgePoints.Add(new Point(x, y));
                    }
                }
            }

            return edgePoints;
        }
        private static List<PointF> _fitSmoothCurve(List<Point> edgePoints)
        {
            List<PointF> smoothCurve = new List<PointF>();

            // Simple curve-fitting example: Smooth using averaging
            for (int i = 1; i < edgePoints.Count - 1; i++)
            {
                Point prev = edgePoints[i - 1];
                Point current = edgePoints[i];
                Point next = edgePoints[i + 1];

                // Average the points for smoothing
                float avgX = (prev.X + current.X + next.X) / 3.0f;
                float avgY = (prev.Y + current.Y + next.Y) / 3.0f;

                smoothCurve.Add(new PointF(avgX, avgY));
            }

            return smoothCurve;
        }
        private Bitmap _createBlackWhiteBitmap(Bitmap bitmap)
        {

            // lets loop through every pixel and count the colors
            var colorCount = new Dictionary<string, int>();

            // this is going to be the new rail bitmap
            using (var railBits = new Bitmap(bitmap.Width, bitmap.Height))
            {
                // Loop through every pixel in the image
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        var color = bitmap.GetPixel(x, y);
                        var hexValue = ColorToHex(color).ToUpper();

                        // now we need to know if the color is one of the rail colors
                        // if it is, we need to change it to make the color black
                        // if not then the pixel should be white

                        var railColors = new List<string>
                        {
                            "#DADEDE".ToUpper(),
                            "#CDD0D4".ToUpper(),
                            "#C1C5C9".ToUpper(),
                        };

                        if (colorCount.ContainsKey(hexValue))
                        {
                            colorCount[hexValue]++;
                        }
                        else
                        {
                            colorCount.Add(hexValue, 1);
                        }

                        // if rail color exists then draw it 
                        if (railColors.Contains(hexValue))
                        {
                            // Change the pixel color to black
                            railBits.SetPixel(x, y, Color.Black);
                        }
                        else
                        {
                            // Change the pixel color to white
                            railBits.SetPixel(x, y, Color.White);
                        }
                    }
                }


                //var sortedByValueAsc = colorCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value).ToArray();

                // loop through each color and see if it is a rail color
                //for (int y = 0; y < bitmap.Height; y++)
                //{
                //    for (int x = 0; x < bitmap.Width; x++)
                //    {
                //        var color = bitmap.GetPixel(x, y);
                //        var hexValue = ColorToHex(color).ToUpper();


                //        if (sortedByValueAsc[int.Parse(lblColor.Text)].Key.ToUpper() == hexValue.ToUpper())
                //        {
                //            railBits.SetPixel(x, y, Color.Black);
                //        }


                //    }
                //}



                //pctOneColorBitmap.Image = new Bitmap(railBits);
                //txtDebug.Text = $"Sucsess: Height: {railBits.Height} Width: {railBits.Width} ";

                return new Bitmap(railBits);
            }
        }



        // Method to convert a Color object to a hex string
        static string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

    }
}
