using Dizignit.Core;

namespace Dizignit.Domain
{
    public class GameMap
    {
        private MapCordinate _topLeft { get; set; } = new MapCordinate(0, 0);
        private MapCordinate _topRight { get; set; } = new MapCordinate(0, 0);
        private MapCordinate _bottomLeft { get; set; } = new MapCordinate(0, 0);
        private MapCordinate _bottomRight { get; set; } = new MapCordinate(0, 0);

        private IEnumerable<MapTile> _gameTiles { get; set; } = Enumerable.Empty<MapTile>();

        public GameMap(MapCordinate topLeft, MapCordinate bottomRight)
        {
            _topLeft = topLeft;
            _bottomLeft = bottomRight;
            _topRight = new MapCordinate(bottomRight.Latitude, topLeft.Longitude);
            _bottomRight = new MapCordinate(bottomRight.Latitude, bottomRight.Longitude);
        }



        //public static GameMap GetTiles()
        //{
        //    // at this point we know that we have a map that is a box
        //    // we need to loop and calculate the center cordinate for each tile
        //    // we also need to make sure we stop tiling when we reach the right edge and bottom of the map





        //    var tileSize = Constants.TileSize; // Size of the tile in pixels
        //    var pixelsPerDegree = Constants.PixelsPerDegree; // Example value, adjust as needed
        //    // Example center coordinate
        //    var centerCoordinate = new MapCordinate(37.7749, -122.4194); // San Francisco coordinates
        //    // Calculate the center of the tile
        //    var tileCenter = TileCalculator.CalculateTileCenter(centerCoordinate, tileSize, pixelsPerDegree);
        //    // Calculate adjacent tiles
        //    var northTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.North);
        //    var southTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.South);
        //    var eastTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.East);
        //    var westTileCenter = TileCalculator.CalculateAdjacentTileCenter(tileCenter, tileSize, pixelsPerDegree, ETileDirection.West);
        //    return map;
        //}
    }
}