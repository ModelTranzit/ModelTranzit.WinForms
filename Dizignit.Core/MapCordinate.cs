namespace Dizignit.Core
{
    public class MapCordinate
    {
        public MapCordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
