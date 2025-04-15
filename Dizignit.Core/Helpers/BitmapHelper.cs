using System.Drawing;

namespace Dizignit.Core.Helpers
{
    public static class BitmapHelper
    {
        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
        }
    }
}
