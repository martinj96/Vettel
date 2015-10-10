﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Core.Entities;

namespace BusinessLogic.MatchRide
{
    public interface IRadiusCalculator
    {
        decimal GetRadius(ICollection<CriticalPoint> criticalPoints);
    }
}