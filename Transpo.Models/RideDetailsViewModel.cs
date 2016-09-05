using System;
using System.Collections.Generic;
using System.Linq;

namespace Transpo.Models
{
    public class RideDetailsViewModel
    {
        public UserViewModel Driver { get; set; }
        public RideViewModel Ride { get; set; }
        public List<CriticalPointDto> Points { get; set; }
        public List<UserViewModel> Riders { get; set; }
        public bool UserIsRider { get; set; }
        public int RideId { get; set; }
    }
}