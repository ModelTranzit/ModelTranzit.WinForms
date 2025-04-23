using Dizignit.Core;
using Dizignit.DAL;

namespace Dizignit.Domain
{
    public class TileGenerator
    {
        public async static Task<IEnumerable<MapTile>> GenerateTiles(MapCordinate topLeft, MapCordinate bottomRight, string requestId)
        {
            var tiles = new List<MapTile>();
            var counter = 0;


            for (double lat = topLeft.Latitude; lat >= bottomRight.Latitude; lat -= Constants.TileSize / Constants.PixelsPerDegree)
            {
                for (double lon = topLeft.Longitude; lon <= bottomRight.Longitude; lon += Constants.TileSize / Constants.PixelsPerDegree)
                {
                    var tileCenter = new MapCordinate(lat + (Constants.TileSize / (2 * Constants.PixelsPerDegree)), lon + (Constants.TileSize / (2 * Constants.PixelsPerDegree)));

                    // get center image
                    var request = new HttpApiRequest(tileCenter, Constants.TileSize, Constants.Zoom);
                    var image = await request.GetImageAsync();
                    var tileRenderer = new TileRenderer(requestId);

                    var fImage = tileRenderer.ConverTileToBlack(image, new MapBitDepth());

                    // lets save thse images to the hard drive 
                    var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"\\images\\tile_{counter}.bmp");
                    
                    
                    
                    await File.WriteAllBytesAsync(filePath, fImage);


                    counter++;
                    var mapTile = new MapTile(tileCenter);
                    tiles.Add(new MapTile(tileCenter));
                }
            }
            return tiles;
        }
    }
}
