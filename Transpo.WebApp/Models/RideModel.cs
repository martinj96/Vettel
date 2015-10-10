using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transpo.WebApp.Models
{
    public class Point
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
    public class Time
    {
        public int Hour { get; set; }
        public int Minutes { get; set; }
    }

    public class RideModel
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public decimal PricePerPassenger { get; set; }
        public int SeatsLeft { get; set; }
        public decimal Length { get; set; }
        public int Detour { get; set; }
        public DateTime DepartureDate { get; set; }
        public Time TimeDeparture { get; set; }
        public Time ReturnTimeDeparture { get; set; }
        public string Description { get; set; }
        public ICollection<Point> Waypoints { get; set; }
        public DateTime ReturnDepartureDate { get; set; }
    }
}