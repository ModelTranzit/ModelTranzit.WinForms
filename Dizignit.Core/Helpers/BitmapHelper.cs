using System.Drawing;

namespace Dizignit.Core.Helpers
{
    public static class BitmapHelper
    {
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the bitmap to the memory stream in a chosen format (e.g., PNG)
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                // Return the byte array
                return ms.ToArray();
            }
        }

        public static Bitmap ByteArrayToBitmap(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return new Bitmap(ms);
            }
        }
    }
}
