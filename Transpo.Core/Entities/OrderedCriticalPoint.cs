using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Core.Entities
{
    public class OrderedCriticalPoint : BaseEntity
    {
        public int Order { get; set; }
        public int RideId { get; set; }
        public int CriticalPointId { get; set; }
        public virtual Ride Ride { get; set; }
        public virtual CriticalPoint CriticalPoint { get; set; }
    }
}
