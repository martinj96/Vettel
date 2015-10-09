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

            var user = (HttpContext.User as CustomPrincipal);
            /*if( userId has access){
                viewModel.UserHasPermission = true;
                we give one view
            }else{
                viewModel.UserHasPermission = false;
                return View(DetailsPermissionsView, ViewModel())
            }
             */
            if (user.UserId == ride.Driver.id)
            {
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
            viewModel.UserIsRider = false;
            foreach (var rider in ride.Riders)
            {
                if (rider.id == user.UserId)
                    viewModel.UserIsRider = true;
            }
            return View(viewModel);
        }
        public ActionResult CreateRide(RideModel ride)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = Request.Cookies[cookieName];
            
            if (authCookie == null)
                throw new UnauthorizedAccessException();

            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            CustomPrincipalSerializedModel userModel = new JavaScriptSerializer().Deserialize<CustomPrincipalSerializedModel>(authTicket.UserData);
            var userId = userModel.UserId;

            var rdto = ConvertRideModelToDto(userId, ride);
            _rideService.AddRide(rdto);

            return View("Create");
        }

        private RideDto ConvertRideModelToDto(int userId, RideModel ride)
        {
            User user = _userService.GetUserById(userId);
            
            ICollection<CriticalPointDto> cpdto = new List<CriticalPointDto>();
            cpdto.Add(new CriticalPointDto {
                Latitude = ride.StartPoint.Latitude,
                Longitude = ride.StartPoint.Longitude
            });

            foreach (var point in ride.Waypoints)
	        {
		        cpdto.Add(new CriticalPointDto {
                    Latitude = point.Latitude,
                    Longitude = point.Longitude
                });
	        }
            
            cpdto.Add(new CriticalPointDto {
                Latitude = ride.EndPoint.Latitude,
                Longitude = ride.EndPoint.Longitude
            });

            RideDto rdto = new RideDto
            {
                Driver = user,
                Departure = ride.DepartureDate,
                CriticalPoints = cpdto
            };

            return rdto;
        }
    }
}