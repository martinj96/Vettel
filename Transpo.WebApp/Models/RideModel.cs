using System;
using System.Collections.Generic;
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
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public decimal? PricePerPassenger { get; set; }
        public int? SeatsLeft { get; set; }
        public decimal? Length { get; set; }
        public int? Detour { get; set; }
        public DateTime DepartureDate { get; set; }
        public Time TimeDeparture { get; set; }
        public Time ReturnTimeDeparture { get; set; }
        public string Description { get; set; }
        public ICollection<Point> Waypoints { get; set; }
        public DateTime? ReturnDepartureDate { get; set; }
        public UserViewModel Driver { get; set; }
        public int RideId { get; set; }

        public RideModel() { }

        public RideModel(Ride ride)
        {
            StartPoint = new Point { Longitude = ride.OrderedCriticalPoints.First().CriticalPoint.Longitude, Latitude = ride.OrderedCriticalPoints.First().CriticalPoint.Latitude, Name = ride.OrderedCriticalPoints.First().CriticalPoint.Name };
            EndPoint = new Point { Longitude = ride.OrderedCriticalPoints.Last().CriticalPoint.Longitude, Latitude = ride.OrderedCriticalPoints.Last().CriticalPoint.Latitude, Name = ride.OrderedCriticalPoints.Last().CriticalPoint.Name };
            PricePerPassenger = ride.PricePerPassenger;
            Detour = ride.Detour;
            Description = ride.Description;
            SeatsLeft = ride.SeatsLeft;
            DepartureDate = ride.Departure;
            Waypoints = (from w in ride.OrderedCriticalPoints
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