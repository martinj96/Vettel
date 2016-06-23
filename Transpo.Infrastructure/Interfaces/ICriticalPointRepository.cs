﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.Infrastructure.Data.Interfaces
{
    public interface ICriticalPointRepository : IBaseRepository<CriticalPoint>
    {
        CriticalPoint GetByLatLon(decimal lat, decimal lon);
    }
}