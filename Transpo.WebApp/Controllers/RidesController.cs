using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Transpo.AppServices;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Models;
using Transpo.Core.Entities;
using Transpo.Infrastructure.Data;
using Transpo.WebApp.Models;

namespace Transpo.WebApp.Controllers
{
    [Authorize]
    public class RidesController : BaseController
    {
        // GET: Rides

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            var ride = _rideService.GetById(id);
            if (ride == null || ride.Active == false)
                return Content("Invalid ride.. Place make sure you request an existing ride.");
            var viewModel = new RideDetailsViewModel();
            viewModel.Driver = new UserViewModel(ride.Driver);
            viewModel.Ride = new RideViewModel(ride);
            viewModel.RideId = id;
            viewModel.Points = _rideService.GetRidesSortedCriticalPoints(id);
            var user = (HttpContext.User as CustomPrincipal);
            /*if( userId has access){
                viewModel.UserHasPermission = true;
                we give one view
            }else{
                viewModel.UserHasPermission = false;
                return View(DetailsPermissionsView, ViewModel())
            }
             */

            viewModel.UserIsRider = false;
            if (user.UserId == ride.Driver.id)
            {
                viewModel.UserIsRider = true;
                foreach (var rider in ride.Riders)
                {
                    viewModel.Riders.Add(new UserViewModel(rider));
                }
                /*
                foreach (var requeser in ...){
                    vieModel.RideRequesters.Add(new UserViewModel(requester))
                }
                 */
                // return View("DriverView", viewModel)
            }
            foreach (var rider in ride.Riders)
            {
                if (rider.id == user.UserId)
                    viewModel.UserIsRider = true;
            }
            
            return View(viewModel);
        }
        public void AddMeToRide(int rideId)
        {
            var ride = _rideService.GetById(rideId);
            var user = _userService.GetUserById((HttpContext.User as CustomPrincipal).UserId);
            _rideService.AddMeToRide(user, ride);
        }
        public ActionResult CreateRide(RideModel ride, string returnR)
        {
            var user = (HttpContext.User as CustomPrincipal);

            if (user == null)
                throw new UnauthorizedAccessException();

            var dto = ConvertRideModelToDto(ride, user.UserId, returnR);
            _rideService.AddRide(dto);

            return View("Create");
        }

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
                dto.ReturnDepartureDate = ride.ReturnDepartureDate.Add(time);
            }


            int o = 0;
            var cp = new CriticalPointDto {
                Latitude = ride.StartPoint.Latitude,
                Longitude = ride.StartPoint.Longitude,
                Name = ride.StartPoint.Name
            };
            var ocpDto = new List<OrderedCriticalPointDto>();
            ocpDto.Add(new OrderedCriticalPointDto {
                CriticalPoint = cp,
                Order = o
            });
            o++;
            foreach (var point in ride.Waypoints)
	        {
                if(point.Latitude == 0 && point.Latitude == 0)
                    continue;
                cp.Latitude = point.Latitude;
                cp.Longitude = point.Longitude;
                cp.Name = point.Name;
		        ocpDto.Add(new OrderedCriticalPointDto {
                    CriticalPoint = cp,
                    Order = o
                });
                o++;
	        }
            cp.Latitude = ride.EndPoint.Latitude;
            cp.Longitude = ride.EndPoint.Longitude;
            cp.Name = ride.EndPoint.Name;
            ocpDto.Add(new OrderedCriticalPointDto
            {
                CriticalPoint = cp,
                Order = o
            });
            dto.Waypoints = ocpDto;
            
            return dto;
        }
    }
}