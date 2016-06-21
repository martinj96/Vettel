using BusinessLogic.Distances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.MatchRide
{
    public class RadiusCalculator5Percent : IRadiusCalculator
    {
        public IDistance DistanceMetric { get; set; }
        private static decimal RadiusMargin = 0.05M;

        public RadiusCalculator5Percent()
        {
            DistanceMetric = new Haversine();
        }

        public decimal GetRadius(ICollection<Transpo.Core.Entities.CriticalPoint> criticalPoints)
        {
            decimal length = 0;
            for (int i = 1; i < criticalPoints.Count; i++)
            {
                length += DistanceMetric.GetDistance(criticalPoints.ElementAt(i - 1), criticalPoints.ElementAt(i));
            }

            return length * RadiusMargin;
        }
    }
}
