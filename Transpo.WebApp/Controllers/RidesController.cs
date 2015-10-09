using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Models;
using Transpo.Core.Entities;
using Transpo.Infrastructure.Data;
using Transpo.WebApp.Models;

namespace Transpo.WebApp.Controllers
{
    public class RidesController : BaseController
    {
        // GET: Rides
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
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

        public RideDto ConvertRideModelToDto(int userId, RideModel ride)
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