using Microsoft.AspNet.Identity;
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
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Entities;
using Transpo.WebApp.Models;

namespace Transpo.WebApp.Controllers
{
    [Authorize]
    public class RidesController : BaseController
    {
        // GET: Rides

        [Authorize]
        public ActionResult Create()
        {
            var user = UserManager.FindById(User.Identity.GetUserId()).User;

            if (String.IsNullOrEmpty(user.Phone))
            {
                return RedirectToAction("Index", "Manage", new { @message = ManageController.ManageMessageId.PhoneRequired });
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            Ride ride = Service.GetRideService().GetById(id);
            if (ride == null || ride.Active == false)
                return Content("Invalid ride.. Place make sure you request an existing ride.");

            RideDetailsViewModel viewModel = ConvertRideToViewModel(ride, id);
            
            return View(viewModel);
        }

        [Authorize]
        public void AddMeToRide(int rideId)
        {
            var ride = Service.GetRideService().GetById(rideId);
            var user = UserManager.FindById(User.Identity.GetUserId());
            //var user = _userService.GetUserById((HttpContext.User as CustomPrincipal).UserId);
            Service.GetRideService().AddMeToRide(user.User, ride);
        }

        [Authorize]
        public ActionResult CreateRide(RideModel ride, string returnR)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (user == null)
                throw new UnauthorizedAccessException();

            var dto = ConvertRideModelToDto(ride, user.User.id, returnR);
            Ride r = Service.GetRideService().AddRide(dto);

            return View("Details", ConvertRideToViewModel(r, r.id));
        }

        [Authorize]
        public ActionResult MyRides()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (user == null)
                throw new UnauthorizedAccessException();

            var rides = new SearchResultModel(user.User);

            return View(rides);
        }

        [Authorize]
        public ActionResult DeleteRide(int id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var ride = Service.GetRideService().GetById(id);

            if (user.User.id != ride.DriverId)
            {
                return RedirectToAction("MyRides");
            }

            Service.GetRideService().DeleteRide(id);
            return RedirectToAction("MyRides");
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
                if(point.Latitude.CompareTo(Decimal.Zero) == 0 && point.Latitude.CompareTo(Decimal.Zero) == 0)
                    continue;
                cp = new CriticalPointDto
                {
                    Latitude = point.Latitude,
                    Longitude = point.Longitude,
                    Name = point.Name
                };
		        ocpDto.Add(new OrderedCriticalPointDto {
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
            var viewModel = new RideDetailsViewModel();
            viewModel.Driver = new UserViewModel(ride.Driver);
            viewModel.Ride = new RideViewModel(ride);
            viewModel.RideId = id;
            viewModel.Points = Service.GetRideService().GetRidesSortedCriticalPoints(id);
            viewModel.UserIsRider = User.Identity.IsAuthenticated
                ? ride.Driver.id == UserManager.FindById(User.Identity.GetUserId()).User.id
                : false;
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