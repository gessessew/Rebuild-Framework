using Rebuild.Extensions;
using System;

namespace Rebuild.Utils
{
    public struct GeoLocation
    {
        public readonly double Latitude;
        public readonly double Longitude;

        public GeoLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double DistanceInKm(GeoLocation other)
        {
            var lat = Math.Sin((other.Latitude - Latitude).DegreeToRadian()/2);
            var lon =  Math.Sin((other.Longitude - Longitude).DegreeToRadian()/2);
            var a = lat * lat + lon * lon * Math.Cos(Latitude.DegreeToRadian()) * Math.Cos(other.Latitude.DegreeToRadian());

            return 6371 * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        }
    }
}
