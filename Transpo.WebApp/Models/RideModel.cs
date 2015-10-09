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

    public class RideModel
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public ICollection<Point> Waypoints { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}