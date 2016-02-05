using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.AppServices;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Repositories;

namespace Transpo.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected CarService _carService;
        protected UserService _userService;
        protected RideService _rideService;

        public BaseController()
        {
            string cnnString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            var dbContext = new TranspoDbContext(cnnString);
            var userRepository = new UserRepository(dbContext);
            _userService = new UserService(userRepository);
            var carRepository = new CarRepository(dbContext);
            _carService = new CarService(carRepository);
            var critialPointRepository = new CriticalPointRepository(dbContext);
            var rideRepository = new RideRepository(dbContext);
            var orderedCriticalPointRepository = new OrderedCriticalPointRepository(dbContext);
            _rideService = new RideService(rideRepository, userRepository, critialPointRepository, orderedCriticalPointRepository);
            var characteristicsRepository = new CharacteristicRepository(dbContext);
        }
    }
}