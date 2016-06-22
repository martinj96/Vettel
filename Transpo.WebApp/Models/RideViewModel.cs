using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.WebApp.Models
{
    public class RideViewModel
    {
        public RideViewModel(Ride r)
        {
            PricePerPassenger = r.PricePerPassenger;
            SeatsLeft = r.SeatsLeft;
            Length = r.Length;
            Detour = r.Detour;
            Departure = r.Departure;
            Description = r.Description;
        }
        public decimal PricePerPassenger { get; set; }
        public int SeatsLeft { get; set; }
        public decimal Length { get; set; }
        public int Detour { get; set; }
        public DateTime Departure { get; set; }
        public string Description { get; set; }
    }
}
