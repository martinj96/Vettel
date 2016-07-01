using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transpo.AppServices;
using Transpo.AppServices.Factories;
using Transpo.AppServices.Interfaces;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Identity;
using Transpo.Infrastructure.Data.Repositories;

namespace Transpo.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected IServiceFactory _service;
        protected AppSignInManager _signInManager;
        protected AppUserManager _userManager;

        public BaseController()
        {
            //string cnnString = DAUtilities.ConnectionString;
            //var dbContext = new TranspoDbContext(cnnString);
            //service = new ServiceFactory(cnnString);

            //var userRepository = new UserRepository(dbContext);
            //_userService = new UserService(userRepository);
            //var carRepository = new CarRepository(dbContext);
            //_carService = new CarService(carRepository);
            //var criticalPointRepository = new CriticalPointRepository(dbContext);
            //var rideRepository = new RideRepository(dbContext);
            //var orderedCriticalPointRepository = new OrderedCriticalPointRepository(dbContext);
            //_rideService = new RideService(rideRepository, userRepository, criticalPointRepository, orderedCriticalPointRepository);
            //var characteristicsRepository = new CharacteristicRepository(dbContext);
            //var messageRepository = new MessageRepository(dbContext);
            //_messageService = new MessageService(messageRepository, userRepository);
        }

        public IServiceFactory Service
        {
            get
            {
                if (_service != null)
                    return _service;

                _service = new ServiceFactory(DAUtilities.ConnectionString);

                return _service;
            }
            private set
            {
                _service = value;
            }
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