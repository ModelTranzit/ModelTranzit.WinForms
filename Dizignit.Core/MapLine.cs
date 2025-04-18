namespace Dizignit.Core
{
    public class MapLine
    {
        public IDictionary<MapCordinate, MapPoint> Points { get; set; }

        public MapLine()
        {
            Points = new Dictionary<MapCordinate, MapPoint>();
        }
    }
}
