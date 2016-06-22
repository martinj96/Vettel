using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.AppServices;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Identity;
using Transpo.Infrastructure.Data.Repositories;

namespace Transpo.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected CarService _carService;
        protected RideService _rideService;
        protected UserService _userService;
        protected AppSignInManager _signInManager;
        protected AppUserManager _userManager;

        public BaseController()
        {
            string cnnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            var dbContext = new TranspoDbContext(cnnString);
            var userRepository = new UserRepository(dbContext);
            _userService = new UserService(userRepository);
            var carRepository = new CarRepository(dbContext);
            _carService = new CarService(carRepository);
            var criticalPointRepository = new CriticalPointRepository(dbContext);
            var rideRepository = new RideRepository(dbContext);
            var orderedCriticalPointRepository = new OrderedCriticalPointRepository(dbContext);
            _rideService = new RideService(rideRepository, userRepository, criticalPointRepository, orderedCriticalPointRepository);
            var characteristicsRepository = new CharacteristicRepository(dbContext);
        }

        public AppSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}