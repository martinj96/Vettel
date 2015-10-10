using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.AppServices.DTOs;

namespace Transpo.WebApp.Models
{
    public class RideDetailsViewModel
    {
        public UserViewModel Driver { get; set; }

        public RideViewModel Ride { get; set; }
        public List<CriticalPointDto> Points { get; set; }
        public List<UserViewModel> Riders { get; set; }
        public bool UserIsRider { get; set; }
    }
}