using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Identity;

namespace Transpo.Infrastructure.Data.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Reviews = new List<Review>();
            Rides = new List<Ride>();
            HasAccessToRides = new List<Ride>();
        }

        public string Name { get; set; }
        public int? Gender { get; set; }
        public int? Age { get; set; }
        public string Link { get; set; }
        public long FacebookId { get; set; }
        public decimal Rating { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PictureUrl { get; set; }
        public virtual Car Car { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Review> GivenReviews { get; set; }
       // public virtual ICollection<Characteristic> Characteristics { get; set; }
        public virtual ICollection<Ride> MyRides { get; set; }
        public virtual ICollection<Ride> Rides { get; set; }
        public virtual ICollection<Ride> HasAccessToRides { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
    }
}
