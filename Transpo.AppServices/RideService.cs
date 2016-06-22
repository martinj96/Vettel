using Transpo.BusinessLogic.MatchRide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.AppServices.DTOs;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.AppServices
{
    public class RideService
    {
        private IRideRepository _rideRepository;
        private User user;
        public IRadiusCalculator RadiusCalculator { get; set; }
        private IUserRepository _userRepository;
        private ICriticalPointRepository _criticalPointRepository;
        private IOrderedCriticalPointRepository _orderedCriticalPointRepository;

        public RideService(IRideRepository rideRepository, IUserRepository userRepository, 
            ICriticalPointRepository criticalPointRepository, IOrderedCriticalPointRepository orderedCriticalPoint)
        {
            RadiusCalculator = new RadiusCalculator5Percent();
            _rideRepository = rideRepository;
            _userRepository = userRepository;
            _criticalPointRepository = criticalPointRepository;
            _orderedCriticalPointRepository = orderedCriticalPoint;
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
            return _rideRepository.GetRides(criticalPoints, radius);
        }

        public ICollection<Ride> GetMyRides()
        {
            return _rideRepository.GetRides(user);
        }

        public Ride AddRide(RideDto r)
        {
            var ride = new Ride();
            ride.Departure = r.DepartureDate;
            ride.Description = r.Description;
            ride.Detour = r.Detour;
            ride.Driver = _userRepository.GetById(r.DriverId);
            ride.Length = r.Length;
            ride.PricePerPassenger = r.PricePerPassenger;
            ride.SeatsLeft = r.SeatsLeft;
            _rideRepository.Add(ride);
            _rideRepository.Save();
            ride.OrderedCriticalPoints = AddCriticalPoints(r.Waypoints, ride);
            return ride;
        }
        private ICollection<OrderedCriticalPoint> AddCriticalPoints(List<OrderedCriticalPointDto> points, Ride r){
            AddNonExistingCriticalPoints(points);
            var result = new List<OrderedCriticalPoint>();
            foreach (var point in points)
            {
                var ocp = new OrderedCriticalPoint();
                CriticalPoint cp = _criticalPointRepository.GetByLatLon(point.CriticalPoint.Latitude, point.CriticalPoint.Longitude);
                ocp.CriticalPoint = cp;
                ocp.Order = point.Order;
                ocp.Ride = r;
                result.Add(ocp);
                _orderedCriticalPointRepository.Add(ocp);
            }
            _orderedCriticalPointRepository.Save();
            return result;
        }
        private void AddNonExistingCriticalPoints(List<OrderedCriticalPointDto> points)
        {
            Boolean pointProcessed = false;
            foreach (var point in points)
            {
                CriticalPoint cp = _criticalPointRepository.GetByLatLon(point.CriticalPoint.Latitude, point.CriticalPoint.Longitude);
                if (cp == null)
                {
                    pointProcessed = true;
                    cp = new CriticalPoint();
                    cp.Latitude = decimal.Round(point.CriticalPoint.Latitude, 5);
                    cp.Longitude = decimal.Round(point.CriticalPoint.Longitude, 5);
                    cp.Name = point.CriticalPoint.Name;
                    _criticalPointRepository.Add(cp);
                }
            }
            if (pointProcessed)
                _criticalPointRepository.Save();
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
        public List<CriticalPointDto> GetRidesSortedCriticalPoints(int id)
        {
            var originalList = _orderedCriticalPointRepository.GetCriticalPointsByRideId(id);
            originalList.Sort(delegate(OrderedCriticalPoint x, OrderedCriticalPoint y)
            {
                if (x.Order > y.Order)
                    return 1;
                else
                    return -1;
            });
            var result = new List<CriticalPointDto>();
            foreach (var point in originalList)
            {
                result.Add(new CriticalPointDto(point));
            }
            return result;
        }
        public void AddMeToRide(User u, Ride r)
        {
            var alreadyIn = false;
            foreach (var rider in r.Riders)
            {
                if (u.id == rider.id)
                    alreadyIn = true;
            }
            if (!alreadyIn)
            {
                _rideRepository.Edit(r);
                r.Riders.Add(u);
                _rideRepository.Save();
            }
        }
    }
}
