using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.Core.Interfaces
{
    public interface IRideRepository : IBaseRepository<Ride>
    {
        ICollection<Ride> getRides(ICollection<CriticalPoint> criticalPoints, decimal radius);

        ICollection<Ride> getRides(User user);
    }
}
