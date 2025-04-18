namespace Dizignit.Core
{
    public class MapCordinate
    {
        public MapCordinate(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
