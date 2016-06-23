﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Infrastructure.Data.Entities
{
    public abstract class BaseEntity
    {
        public int id { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual bool Active { get; set; }
        public BaseEntity()
        {
            DateCreated = DateTime.Now;
            Active = true;
        }
    }
}