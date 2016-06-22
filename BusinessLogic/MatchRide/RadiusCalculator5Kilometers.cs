using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.BusinessLogic.Distances;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.BusinessLogic.MatchRide
{
    class RadiusCalculator5Kilometers : IRadiusCalculator
    {
        public IDistance DistanceMetric { get; set; }
        private const decimal RadiusMargin = 5;

        public RadiusCalculator5Kilometers()
        {
            DistanceMetric = new Haversine();
        }

        public decimal GetRadius(ICollection<CriticalPoint> criticalPoints)
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
