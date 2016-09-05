using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transpo.Models
{
    public class RideViewModel
    {
        public decimal? PricePerPassenger { get; set; }
        public int? SeatsLeft { get; set; }
        public decimal? Length { get; set; }
        public int? Detour { get; set; }
        public DateTime Departure { get; set; }
        public string Description { get; set; }
    }
}
