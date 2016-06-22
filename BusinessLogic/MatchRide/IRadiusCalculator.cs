using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.BusinessLogic.MatchRide
{
    public interface IRadiusCalculator
    {
        decimal GetRadius(ICollection<CriticalPoint> criticalPoints);
    }
}
