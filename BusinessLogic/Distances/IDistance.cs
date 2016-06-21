using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace BusinessLogic.Distances
{
    public interface IDistance
    {
        decimal GetDistance(CriticalPoint p1, CriticalPoint p2);
    }
}
