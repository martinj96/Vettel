using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Transpo.AppServices.Factories;
using Transpo.AppServices.Interfaces;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Identity;

namespace Transpo.API.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IServiceFactory _service;
        protected AppSignInManager _signInManager;
        protected AppUserManager _userManager;

        public BaseApiController() { }

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
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<AppSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}
