using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.AppServices;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Factories;
using Transpo.AppServices.Interfaces;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.API.Models
{
    public class SearchResultModel : SearchModel
    {
        public IEnumerable<RideModel> Rides { get; set; }
        private IServiceFactory serviceFactory;

        public SearchResultModel(User user)
        {
            serviceFactory = new ServiceFactory(DAUtilities.ConnectionString);
            RideService service = serviceFactory.GetRideService();
            List<RideModel> rs = new List<RideModel>();
            ICollection<Ride> col = service.GetMyRides(user);

            foreach (Ride ride in col)
            {
                RideModel rideModel = new RideModel(ride);
                rs.Add(rideModel);
            }
            Rides = rs.OrderByDescending(x => x.DepartureDate).AsEnumerable();
        }

        public SearchResultModel(SearchModel searchModel)
        {
            serviceFactory = new ServiceFactory(DAUtilities.ConnectionString);
            List<RideModel> rs = new List<RideModel>();
            this.FromCityName = searchModel.FromCityName;
            this.FromCountryShortCode = searchModel.FromCountryShortCode;
            this.FromLatitude = searchModel.FromLatitude;
            this.FromLongitude = searchModel.FromLongitude;

            this.ToCityName = searchModel.ToCityName;
            this.ToCountryShortCode = searchModel.ToCountryShortCode;
            this.ToLatitude = searchModel.ToLatitude;
            this.ToLongitude = searchModel.ToLongitude;

            CriticalPointDto start = new CriticalPointDto();
            start.Longitude = (decimal)FromLongitude;
            start.Latitude = (decimal)FromLatitude;
            start.Name = FromCityName;

            CriticalPointDto end = new CriticalPointDto();
            end.Longitude = (decimal)ToLongitude;
            end.Latitude = (decimal)ToLatitude;
            end.Name = ToCityName;

            ICollection<CriticalPointDto> criticalPoints = new LinkedList<CriticalPointDto>();
            criticalPoints.Add(start);
            criticalPoints.Add(end);

            RideService service = serviceFactory.GetRideService();
            ICollection<Ride> col = service.GetRides(criticalPoints);

            foreach (Ride ride in col)
            {
                RideModel rideModel = new RideModel(service.GetById(ride.id));
                rs.Add(rideModel);
            }
            if (searchModel.Date.HasValue)
            {
                Rides = rs.Where(x => x.DepartureDate.Date.CompareTo(searchModel.Date.Value.Date) == 0).ToList();
            }
            else
            {
                Rides = rs;
            }
        }

        public SearchResultModel() 
        {
            serviceFactory = new ServiceFactory(DAUtilities.ConnectionString);
            List<RideModel> rs = new List<RideModel>();

            RideService service = serviceFactory.GetRideService();
            ICollection<Ride> col = service.GetAllRides();

            foreach (Ride ride in col)
            {
                RideModel rideModel = new RideModel(ride);
                rs.Add(rideModel);
            }
            Rides = rs.Where(x => x.DepartureDate >= DateTime.Now).OrderBy(x => x.DepartureDate).AsEnumerable();
        }
    }
}