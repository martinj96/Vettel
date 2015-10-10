using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.AppServices.DTOs;
using Transpo.Core.Entities;

namespace Transpo.WebApp.Models
{
    public class RideViewModel
    {
        public RideViewModel(Ride ride)
        {
            Departure = ride.Departure;
            Description = ride.Description;
            Detour = ride.Detour;
            Length = ride.Length;
            PricePerPassenger = ride.PricePerPassenger;
            SeatsLeft = ride.SeatsLeft;
        }
        public decimal PricePerPassenger { get; set; }
        public int SeatsLeft { get; set; }
        public decimal Length { get; set; }
        public int Detour { get; set; }
        public DateTime Departure { get; set; }
        public string Description { get; set; }
        public List<CriticalPointDto> CriticalPoints { get; set; }
    }
}