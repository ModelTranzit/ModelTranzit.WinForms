using Dizignit.Core.Enums;

namespace Dizignit.Core
{
    public class Tile
    {
        // This class should encapsulate the idea that a tile is a bitmap that relates to other tiles  
        // that is generated from some sort of GPS data  
        // and that it is used to create a map tile for a given location  
        // this is a tool that is used to create map tiles for a given location  


        // THIS TILE IS ALWAYS SQUARE 


        public byte[] Bitmap { get; set; } // Bitmap data  
        public byte[] ColorMap { get; set; } // each bit is a color in the color map  
        public byte[] Pixels { get; set; }
        public int Size { get; set; } // in pixels^2  
        public string FilePath { get; set; }
        public double Latitude { get; set; } // in degrees  
        public double Longitude { get; set; } // in degrees
        private EColorDepth _colorDepth { get; set; } // in bits

        public Tile()
        {
            _colorDepth = EColorDepth.UnsupportedBitDepth;
        }

        private void _calulateBitDebth()
        {

        }
    }
}
