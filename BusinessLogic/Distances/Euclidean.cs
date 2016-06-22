using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.BusinessLogic.Distances
{
    class Euclidean : IDistance
    {
        public decimal GetDistance(CriticalPoint p1, CriticalPoint p2)
        {
            return (decimal)(Math.Sqrt(Math.Pow((double)(p1.Longitude - p2.Longitude), 2) + Math.Pow((double)(p1.Latitude - p2.Latitude), 2)));
        }
    }
}
