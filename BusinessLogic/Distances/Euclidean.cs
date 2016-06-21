using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Distances
{
    class Euclidean : IDistance
    {

        public decimal GetDistance(Transpo.Core.Entities.CriticalPoint p1, Transpo.Core.Entities.CriticalPoint p2)
        {
            return (decimal)(Math.Sqrt(Math.Pow((double)(p1.Longitude - p2.Longitude), 2) + Math.Pow((double)(p1.Latitude - p2.Latitude), 2)));
        }
    }
}
