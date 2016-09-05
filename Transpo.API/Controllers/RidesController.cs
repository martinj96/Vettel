using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;
using Transpo.API.Models;
using Transpo.AppServices;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Models;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Entities;
using Transpo.API.Filters;

namespace Transpo.API.Controllers
{
    [BasicAuthentication]
    public class RidesController : BaseApiController
    {
        // GET: Rides
        [AllowAnonymous]
        [Route("api/rides/details")]
        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            Ride ride = Service.GetRideService().GetById(id);
            if (ride == null || ride.Active == false)
                return InternalServerError();

            RideDetailsViewModel viewModel = ConvertRideToViewModel(ride, id);

            return Ok(viewModel);
        }

        //public void AddMeToRide(int rideId)
        //{
        //    var ride = Service.GetRideService().GetById(rideId);
        //    var user = UserManager.FindById(User.Identity.GetUserId());
        //    //var user = _userService.GetUserById((HttpContext.User as CustomPrincipal).UserId);
        //    Service.GetRideService().AddMeToRide(user.User, ride);
        //}

        [Authorize]
        [Route("api/rides/create")]
        [HttpPost]
        public IHttpActionResult Create([FromBody] RideModel ride)
        {
            var identity = (HttpContext.Current.User.Identity);
            var user = ((ApiIdentity)identity);

            if (user == null)
                return InternalServerError();

            var dto = ConvertRideModelToDto(ride, user.User.id, ride.ReturnRide);
            Ride r = Service.GetRideService().AddRide(dto);

            return Ok(ConvertRideToViewModel(r, r.id));
        }

        [Authorize]
        [Route("api/rides/myrides")]
        [HttpGet]
        public IHttpActionResult MyRides()
        {
            var identity = (HttpContext.Current.User.Identity);
            var user = ((ApiIdentity)identity);

            if (user == null)
                throw new UnauthorizedAccessException();

            var rides = new SearchResultModel(user.User);

            return Ok(rides);
        }

        [Authorize]
        [Route("api/rides/deleteride")]
        [HttpGet]
        public IHttpActionResult DeleteRide(int id)
        {
            var identity = (HttpContext.Current.User.Identity);
            var user = ((ApiIdentity)identity);

            var ride = Service.GetRideService().GetById(id);

            if (user.User.id != ride.DriverId)
            {
                return InternalServerError();
            }

            Service.GetRideService().DeleteRide(id);
            return Ok();
        }

        #region Helpers
        private RideDto ConvertRideModelToDto(RideModel ride, int userId, string returnRide)
        {
            var dto = new RideDto();

            dto.Length = ride.Length;
            dto.Description = ride.Description;
            dto.Detour = ride.Detour;
            dto.DriverId = userId;
            dto.PricePerPassenger = ride.PricePerPassenger;
            dto.SeatsLeft = ride.SeatsLeft;

            if (returnRide == "true")
                dto.ReturnRide = true;
            else
                dto.ReturnRide = false;

            var time = new TimeSpan(ride.TimeDeparture.Hour, ride.TimeDeparture.Minutes, 0);
            dto.DepartureDate = ride.DepartureDate.Add(time);

            if (dto.ReturnRide)
            {
                time = new TimeSpan(ride.ReturnTimeDeparture.Hour, ride.ReturnTimeDeparture.Minutes, 0);
                dto.ReturnDepartureDate = ride.ReturnDepartureDate.Value.Add(time);
            }


            int o = 0;
            var cp = new CriticalPointDto
            {
                Latitude = ride.StartPoint.Latitude,
                Longitude = ride.StartPoint.Longitude,
                Name = ride.StartPoint.Name
            };
            var ocpDto = new List<OrderedCriticalPointDto>();
            ocpDto.Add(new OrderedCriticalPointDto
            {
                CriticalPoint = cp,
                Order = o
            });
            o++;
            foreach (var point in ride.Waypoints)
            {
                if (point.Latitude.CompareTo(Decimal.Zero) == 0 && point.Latitude.CompareTo(Decimal.Zero) == 0)
                    continue;
                cp = new CriticalPointDto
                {
                    Latitude = point.Latitude,
                    Longitude = point.Longitude,
                    Name = point.Name
                };
                ocpDto.Add(new OrderedCriticalPointDto
                {
                    CriticalPoint = cp,
                    Order = o
                });
                o++;
            }
            cp = new CriticalPointDto
            {
                Latitude = ride.EndPoint.Latitude,
                Longitude = ride.EndPoint.Longitude,
                Name = ride.EndPoint.Name
            };
            ocpDto.Add(new OrderedCriticalPointDto
            {
                CriticalPoint = cp,
                Order = o
            });
            dto.Waypoints = ocpDto;

            return dto;
        }
        private RideDetailsViewModel ConvertRideToViewModel(Ride ride, int id)
        {
            var identity = (HttpContext.Current.User.Identity);
            var user = ((ApiIdentity)identity);

            var viewModel = new RideDetailsViewModel();
            viewModel.Driver = new UserViewModel(ride.Driver);
            viewModel.Ride = new RideViewModel(ride);
            viewModel.RideId = id;
            viewModel.Points = Service.GetRideService().GetRidesSortedCriticalPoints(id);
            viewModel.UserIsRider = ride.Driver.id == user.User.id;
            //var user = UserManager.FindById(User.Identity.GetUserId()).User;
            ////var user = (HttpContext.User as CustomPrincipal);
            ///*if( userId has access){
            //    viewModel.UserHasPermission = true;
            //    we give one view
            //}else{
            //    viewModel.UserHasPermission = false;
            //    return View(DetailsPermissionsView, ViewModel())
            //}
            // */

            //viewModel.UserIsRider = false;
            //if (user.id == ride.Driver.id)
            //{
            //    viewModel.UserIsRider = true;
            //    foreach (var rider in ride.Riders)
            //    {
            //        viewModel.Riders.Add(new UserViewModel(rider));
            //    }
            //    /*
            //    foreach (var requeser in ...){
            //        vieModel.RideRequesters.Add(new UserViewModel(requester))
            //    }
            //     */
            //    // return View("DriverView", viewModel)
            //}
            //foreach (var rider in ride.Riders)
            //{
            //    if (rider.id == user.id)
            //        viewModel.UserIsRider = true;
            //}

            return viewModel;
        }
        #endregion
    }
}