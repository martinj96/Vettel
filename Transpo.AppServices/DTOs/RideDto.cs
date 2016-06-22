using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transpo.AppServices.DTOs
{
    public class RideDto
    {
        public decimal PricePerPassenger { get; set; }
        public int DriverId { get; set; }
        public int SeatsLeft { get; set; }
        public decimal Length { get; set; }
        public int Detour { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Description { get; set; }
        public List<OrderedCriticalPointDto> Waypoints { get; set; }
        public bool ReturnRide { get; set; }
        public DateTime ReturnDepartureDate { get; set; }
    }
}
