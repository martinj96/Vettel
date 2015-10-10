﻿using System;
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
        private IUserRepository _userRepository;

        public RideService(IRideRepository rideRepository, IUserRepository userRepository)
        {
            _rideRepository = rideRepository;
            _userRepository = userRepository;
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
            ride = CreateRide(ride);

            _rideRepository.Edit(ride);

            foreach (var point in r.Waypoints)
            {
                ride.OrderedCriticalPoints.Add(new OrderedCriticalPoint);
            }

            _rideRepository.Add(ride);
            _rideRepository.Save();
            return ride;*/
            var Ride = new Ride();
            return Ride;
        }
        private ICollection<OrderedCriticalPoint> AddCriticalPoints(List<OrderedCriticalPoint> points){

        }
        private Ride CreateRide(Ride r)
        {
            _rideRepository.Add(r);
            _rideRepository.Save();
            return r;
        }
        private 

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
            var ride = GetById(id);
            var originalList = new List<OrderedCriticalPoint>();
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
    }
}