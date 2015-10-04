using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Core.Entities
{
    public class CriticalPoint : BaseEntity
    {
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public String Name { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
    }
}
