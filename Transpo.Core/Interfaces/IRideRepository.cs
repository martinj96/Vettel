﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace Transpo.Core.Interfaces
{
    public interface IRideRepository : IBaseRepository<Ride>
    {
        ICollection<Ride> GetRides(ICollection<CriticalPoint> criticalPoints, decimal radius);

        ICollection<Ride> GetRides(User user);
    }
}
