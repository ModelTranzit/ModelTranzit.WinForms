using Dizignit.Core;
using Dizignit.Core.Enums;

namespace Dizignit.Domain
{
    public class MapTile
    {
        public byte[] Pixels { get; set; }
        public MapCordinate Cordinate { get; set; }
        public int Zoom { get; set; }
        public ETileType ImageType { get; set; }
        public int Size { get; set; }

        public MapTile(byte[] pixels, MapCordinate cordinate, int zoom, int size, ETileType imageType)
        {
            Cordinate = new MapCordinate(cordinate.Latitude, cordinate.Longitude);
            Pixels = new byte[size * size];
            Pixels = pixels;
            Zoom = zoom;
            ImageType = imageType;
            Size = size;
        }

        public MapTile GetTile(TileRenderer tileRenderer, byte[] pixels)
        {
            return new MapTile(pixels, Cordinate, Zoom, Size, ImageType);
        }
    }
}
