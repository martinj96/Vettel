using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.BusinessLogic.Distances
{
    class Haversine : IDistance
    {
        public decimal GetDistance(CriticalPoint p1, CriticalPoint p2)
        {
            double R = 6371;

            double dLat = ToRadian(p2.Latitude - p1.Latitude);
            double dLon = ToRadian(p2.Longitude - p1.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadian(p1.Latitude)) * Math.Cos(ToRadian(p2.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;

            return Convert.ToDecimal(d);
        }

        private double ToRadian(decimal value)
        {
            return (Math.PI / 180) * Convert.ToDouble(value);
        }
    }
}
