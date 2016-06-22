using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.BusinessLogic.Distances
{
    public interface IDistance
    {
        decimal GetDistance(CriticalPoint p1, CriticalPoint p2);
    }
}
