namespace Dizignit.Core
{
    public static class Constants
    {
        public const int TileSize = 640; // Size of the tile in pixels 475 seems to line up pretty close without an offset. Use 475 to calculate the tile size offset 
        public const double PixelsPerDegree = 500000; 
        public const double EarthRadiusKm = 6371.0; // Radius of the Earth in kilometers
        public const double DefaultDistanceKm = 10.0; // Default distance for bounding box calculation
        public const int Zoom = 19; // Default zoom level
    }
}
