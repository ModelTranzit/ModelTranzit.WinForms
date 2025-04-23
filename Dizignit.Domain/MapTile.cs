using Dizignit.Core;
using Dizignit.Core.Enums;
using Dizignit.DAL;

namespace Dizignit.Domain
{
    public class MapTile
    {
        public byte[] Pixels { get; set; }
        public MapCordinate Cordinate { get; set; }
        public int Zoom { get; set; }
        public ETileType ImageType { get; set; }
        public int Size { get; set; }

        public MapTile(MapCordinate cordinate, ETileType tileType = ETileType.Unknown)
        {
            Cordinate = new MapCordinate(cordinate.Latitude, cordinate.Longitude);
            Pixels = new byte[Constants.TileSize * Constants.TileSize];
            ImageType = ETileType.Unknown;
            Size = Constants.TileSize;
        }

        public MapTile(byte[] pixels, MapCordinate cordinate, int zoom, int size, ETileType imageType)
        {
            Cordinate = new MapCordinate(cordinate.Latitude, cordinate.Longitude);
            Pixels = pixels;
            Zoom = zoom;
            ImageType = imageType;
            Size = size;
        }

        //public async Task<MapTile> GetTileAsync(TileRenderer tileRenderer)
        //{
        //    var api = new HttpApiRequest(Cordinate, Size, Zoom);
        //    Pixels = tileRenderer.ConverTileToBlack(await api.GetImageAsync(), new MapBitDepth());
        //    return new MapTile(Pixels, Cordinate, Zoom, Size, ImageType);
        //}
    }
}
