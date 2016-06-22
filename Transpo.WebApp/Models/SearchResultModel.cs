using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.AppServices;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Factories;
using Transpo.AppServices.Interfaces;
using Transpo.Infrastructure.Data.Entities;

namespace Transpo.WebApp.Models
{
    public class SearchResultModel : SearchModel
    {
        public IEnumerable<RideModel> Rides { get; set; }
        public IServiceFactory serviceFactory;

        public SearchResultModel(SearchModel searchModel)
        {
            serviceFactory = new ServiceFactory();
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
            Rides = rs;
        }

        public SearchResultModel() 
        {
            Rides = new List<RideModel>();
            serviceFactory = new ServiceFactory();
        }
    }
}