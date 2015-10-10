using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Transpo.AppServices;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Factories;
using Transpo.AppServices.Interfaces;
using Transpo.Core.Entities;

namespace Transpo.WebApp.Models
{
    public class SearchResultModel : SearchModel
    {
        public IEnumerable<RideModel> Rides { get; set; }
        public IServiceFactory serviceFactory;

        public SearchResultModel()
        {
            serviceFactory = new ServiceFactory();
        }

        public SearchResultModel(SearchModel searchModel)
        {
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

            RideService service = serviceFactory.getRideService();
            ICollection<Ride> col = service.GetRides(criticalPoints);

            foreach (Ride ride in col)
            {
                RideModel rideModel = new RideModel();
                rideModel.DepartureDate = ride.Departure;
                rideModel.Description = ride.Description;
                rideModel.Detour = ride.Detour;
                rideModel.Length = ride.Length;
                rideModel.PricePerPassenger = ride.PricePerPassenger;
                rideModel.SeatsLeft = ride.SeatsLeft;
            }
        }

        /*public SearchResultModel() 
        {
            Rides = new List<RideModel>();
        }*/
    }
}