using System;

using System.Collections.Generic;
using System.Text;

namespace GpsPerimeter
{
    public static class DistanceCalculatoor
    {
        public const double EarthRadiusInMiles = 3956.0;
        public const double EarthRadiusInKilometers = 6367.0;
        public static double ToRadian(double val) { return val * (Math.PI / 180); }
        public static double ToDegree(double val) { return val * 180 / Math.PI; }
        public static double DiffRadian(double val1, double val2) { return ToRadian(val2) - ToRadian(val1); }

        /// <summary> 
        /// Calculate the distance between two geocodes. Defaults to using Miles. 
        /// </summary> 

        /// <summary> 
        /// Calculate the distance between two geocodes. 
        /// </summary> 
        public static double CalcDistance(double lat1, double lng1, double lat2, double lng2, double radius)
        {

            return radius * 2 * Math.Asin(Math.Min(1, Math.Sqrt((Math.Pow(Math.Sin((DiffRadian(lat1, lat2)) / 2.0), 2.0) + Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) * Math.Pow(Math.Sin((DiffRadian(lng1, lng2)) / 2.0), 2.0)))));
        }

        public static Double Bearing(double lat1, double lng1, double lat2, double lng2)
        {

            {
                var dLat = lat2 - lat2;
                var dLon = lng2 - lng1;
                var dPhi = Math.Log(Math.Tan(lat2 / 2 + Math.PI / 4) / Math.Tan(lat1 / 2 + Math.PI / 4));
                var q = (Math.Abs(dLat) > 0) ? dLat / dPhi : Math.Cos(lat1);
                
                if (Math.Abs(dLon) > Math.PI)
                {
                    dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
                }
                //var d = Math.Sqrt(dLat * dLat + q * q * dLon * dLon) * R;
                var brng = ToDegree(Math.Atan2(dLon, dPhi));
                return brng;
            }
            //var latitude1 = ToRadian(lat1); ;
            //var latitude2 = ToRadian(lat2);

            //var longitudeDifference = ToRadian(lng2 - lng1);

            //var y = Math.Sin(longitudeDifference) * Math.Cos(latitude2);
            //var x = Math.Cos(latitude1) * Math.Sin(latitude2) -
            //       Math.Sin(latitude1) * Math.Cos(latitude2) * Math.Cos(longitudeDifference);

            //return (ToDegree(Math.Atan2(y, x)) + 360) % 360;
        }
    }


}
