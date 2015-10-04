using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public int Gender { get; set; }
        public decimal Rating { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual Car Car { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Characteristic> Characteristics { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }

    }
}
