﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Core.Entities
{
    public class OrderedCriticalPoint
    {
        public OrderedCriticalPoint()
        {
            Active = true;
        }
        public int id { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public virtual Ride Ride { get; set; }
        public virtual CriticalPoint CriticalPoint { get; set; }
    }
}