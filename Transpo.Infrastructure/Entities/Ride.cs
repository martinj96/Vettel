using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.Infrastructure.Data.Entities
{
    public class Ride : BaseEntity
    {
        public decimal? PricePerPassenger { get; set; }
        public int? SeatsLeft { get; set; }
        public decimal? Length { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Detour { get; set; }
        public DateTime Departure { get; set; }
        public string Description { get; set; }
        public int DriverId { get; set; }
        public virtual User Driver { get; set; }
        public virtual ICollection<User> Riders { get; set; }
        public virtual ICollection<OrderedCriticalPoint> OrderedCriticalPoints { get; set; }
        public virtual ICollection<User> UsersWithAccess { get; set; }

    }
}
