using BusinessLogic.MatchRide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.AppServices.DTOs;
using Transpo.Core.Entities;
using Transpo.Core.Interfaces;

namespace Transpo.AppServices
{
    public class RideService
    {
        private IRideRepository _rideRepository;
        private User user;
        public IRadiusCalculator RadiusCalculator { get; set; }

        public RideService(IRideRepository rideRepository)
        {
            _rideRepository = rideRepository;
            RadiusCalculator = new RadiusCalculator5Percent();
        }

        public ICollection<Ride> GetRides(ICollection<CriticalPointDto> points)
        {
            ICollection<CriticalPoint> criticalPoints = new LinkedList<CriticalPoint>();
            foreach (CriticalPointDto point in points)
            {
                CriticalPoint criticalPoint = new CriticalPoint();
                criticalPoint.Longitude = point.Longitude;
                criticalPoint.Latitude = point.Latitude;
                criticalPoint.Name = point.Name;
                criticalPoints.Add(criticalPoint);
            }
            decimal radius = RadiusCalculator.GetRadius(criticalPoints);
            return _rideRepository.getRides(criticalPoints, radius);
        }

        public ICollection<Ride> GetMyRides()
        {
            return _rideRepository.getRides(user);
        }

        public Ride AddRide(RideDto r)
        {/*
            var ride = new Ride();
            ride.Departure = r.Departure;
            ride.Driver = r.Driver;
            ride.CriticalPoints = new List<CriticalPoint>();
            foreach (var point in r.CriticalPoints)
            {
                ride.CriticalPoints.Add(new CriticalPoint
                {
                    DateCreated = DateTime.Now.Date,
                    Latitude = point.Latitude,
                    Longitude = point.Longitude,
                    Name = point.Name
                });
            }
            _rideRepository.Add(ride);
            _rideRepository.Save();
            return ride;*/
            var Ride = new Ride();
            return Ride;
        }

        public void DeleteRide(int id)
        {
            var ride = _rideRepository.GetById(id);
            _rideRepository.Delete(ride);
            _rideRepository.Save();
        }

        public Ride GetById(int id)
        {
            return _rideRepository.GetById(id);
        }
    }
}
