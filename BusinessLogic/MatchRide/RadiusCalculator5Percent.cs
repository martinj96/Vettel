using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.BusinessLogic.Distances;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.BusinessLogic.MatchRide
{
    public class RadiusCalculator5Percent : IRadiusCalculator
    {
        public IDistance DistanceMetric { get; set; }

        public RadiusCalculator5Percent()
        {
            DistanceMetric = new Euclidean();
        }

        public decimal GetRadius(ICollection<CriticalPoint> criticalPoints)
        {
            decimal length = 0;
            for (int i = 1; i < criticalPoints.Count; i++)
            {
                length += DistanceMetric.GetDistance(criticalPoints.ElementAt(i - 1), criticalPoints.ElementAt(i));
            }

            return length * 5 / 100;
        }
    }
}
