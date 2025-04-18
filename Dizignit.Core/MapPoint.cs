namespace Dizignit.Core
{
    public class MapPoint
    {
        public MapCordinate Cordinate { get; set; }
        // the diameter of the circle that will be drawn around the point
        public int Diameter { get; set; }
        // threshold for the distance between pixels
        public MapBitDepth BitDepth { get; set; }    
    }
}
