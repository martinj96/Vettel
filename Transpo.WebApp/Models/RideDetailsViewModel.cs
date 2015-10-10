using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.Core.Entities;

namespace Transpo.WebApp.Models
{
    public class RideDetailsViewModel
    {
        public RideViewModel Ride { get; set; }
        public UserViewModel Driver { get; set; }
        public bool UserHasPermission { get; set; }
        public bool UserIsRider { get; set; }
        public ICollection<UserViewModel> Riders { get; set; }
        public ICollection<UserViewModel> RideRequesters { get; set; }
    }
}