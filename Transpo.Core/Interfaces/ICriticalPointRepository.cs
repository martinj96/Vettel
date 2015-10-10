using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.Core.Interfaces
{
    public interface ICriticalPointRepository : IBaseRepository<CriticalPoint>
    {
        CriticalPoint getByLatLon(decimal lat, decimal lon);
        void AddOrderedCriticalPoint(OrderedCriticalPoint entity);
        List<OrderedCriticalPoint> getRidesCriticalPoints(int rideId);
    }
}
