using Dizignit.Core;
using Dizignit.Core.Enums;

namespace Dizignit.Domain
{
    public class TileCalculator
    {
        public static MapCordinate CalculateTileCenter(MapCordinate originalCenter, int size, double pixelsPerDegree)
        {
            double latitudeOffset = size / (2 * pixelsPerDegree);
            double longitudeOffset = size / (2 * pixelsPerDegree);
            var newLatitude = originalCenter.Latitude + latitudeOffset;
            var newLongitude = originalCenter.Longitude + longitudeOffset;
            return new MapCordinate(newLatitude, newLongitude);
        }

        public static MapCordinate CalculateAdjacentTileCenter(MapCordinate originalCenter, int size, double pixelsPerDegree, ETileDirection direction)
        {
            double latitudeOffset = 0;
            double longitudeOffset = 0;

            switch (direction)
            {
                case ETileDirection.North:
                    latitudeOffset = size / pixelsPerDegree;
                    break;
                case ETileDirection.South:
                    latitudeOffset = -size / pixelsPerDegree;
                    break;
                case ETileDirection.East:
                    longitudeOffset = size / pixelsPerDegree;
                    break;
                case ETileDirection.West:
                    longitudeOffset = -size / pixelsPerDegree;
                    break;
                default:
                    throw new ArgumentException("Invalid direction");
            }

            var newLatitude = originalCenter.Latitude + latitudeOffset;
            var newLongitude = originalCenter.Longitude + longitudeOffset;

            return new MapCordinate(newLatitude, newLongitude);
        }


        public static MapCordinate CalculateCenter(MapCordinate topLeft, MapCordinate bottomRight)
        {
            double centerLatitude = (topLeft.Latitude + bottomRight.Latitude) / 2;
            double centerLongitude = (topLeft.Longitude + bottomRight.Longitude) / 2;

            return new MapCordinate(centerLatitude, centerLongitude);
        }

        public static (MapCordinate, MapCordinate) CalculateBoundingBox(MapCordinate center)
        {
            double lat = center.Latitude;
            double lon = center.Longitude;

            double latOffset = Constants.DefaultDistanceKm / Constants.DefaultDistanceKm * (180 / Math.PI);
            double lonOffset = Constants.DefaultDistanceKm / Constants.DefaultDistanceKm * (180 / Math.PI) / Math.Cos(lat * Math.PI / 180);

            MapCordinate topLeft = new MapCordinate(lat + latOffset, lon - lonOffset);
            MapCordinate bottomRight = new MapCordinate(lat - latOffset, lon + lonOffset);

            return (topLeft, bottomRight);
        }
    }
}
