using Dizignit.Core;
using Dizignit.Core.Enums;

namespace Dizignit.Domain
{
    public class TileCalculator
    {
        public static MapCordinate CalculateAdjacentTileCenter(MapCordinate originalCenter, int size, decimal pixelsPerDegree, ETileDirection direction)
        {
            decimal latitudeOffset = 0;
            decimal longitudeOffset = 0;

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




    }
}
