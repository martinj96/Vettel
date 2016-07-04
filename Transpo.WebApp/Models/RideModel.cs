using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.WebApp.Models
{
    public class Point
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Name { get; set; }
    }
    public class Time
    {
        public int Hour { get; set; }
        public int Minutes { get; set; }
    }

    public class RideModel
    {
        [Required]
        public Point StartPoint { get; set; }
        [Required]
        public Point EndPoint { get; set; }
        public decimal? PricePerPassenger { get; set; }
        public int? SeatsLeft { get; set; }
        public decimal? Length { get; set; }
        public int? Detour { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public Time TimeDeparture { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection<Point> Waypoints { get; set; }
        public DateTime? ReturnDepartureDate { get; set; }
        public Time ReturnTimeDeparture { get; set; }
        public UserViewModel Driver { get; set; }
        public int RideId { get; set; }

        public RideModel() { }

        public RideModel(Ride ride)
        {
            var ocp = ride.OrderedCriticalPoints.OrderBy(x => x.Order);
            StartPoint = new Point { Longitude = ocp.First().CriticalPoint.Longitude, Latitude = ocp.First().CriticalPoint.Latitude, Name = ocp.First().CriticalPoint.Name };
            EndPoint = new Point { Longitude = ocp.Last().CriticalPoint.Longitude, Latitude = ocp.Last().CriticalPoint.Latitude, Name = ocp.Last().CriticalPoint.Name };
            PricePerPassenger = ride.PricePerPassenger;
            Detour = ride.Detour;
            Description = ride.Description;
            SeatsLeft = ride.SeatsLeft;
            DepartureDate = ride.Departure;
            Waypoints = (from w in ocp
                         select new Point
                         {
                             Longitude = w.CriticalPoint.Longitude,
                             Latitude = w.CriticalPoint.Latitude,
                             Name = w.CriticalPoint.Name
                         }).Skip(1).ToList();
            Waypoints = Waypoints.Take(Waypoints.Count - 1).ToList();
            Driver = new UserViewModel(ride.Driver);
            RideId = ride.id;
        }
    }
}