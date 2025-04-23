namespace Dizignit.Core
{
    public class MapBitDepth
    {
        /// <summary>
        /// Resolution of the tile in pixels TileResolution X TileResolution
        /// </summary>
        public int TileResolution { get; set; }
        /// <summary>
        /// Threshold for the angle of rotation
        /// increment of degrees of rotation to consider as a point 
        /// </summary>
        public int MinDegrees { get; set; }
        /// <summary>
        /// Threshold for the distance between pixels
        /// incremental distance between points to consider as a point
        /// </summary>
        public int MinDistance { get; set; }
        public MapBitDepth()
        {
            TileResolution = Constants.TileSize;
            MinDegrees = 1;
            MinDistance = 90;
        }
    }
}
