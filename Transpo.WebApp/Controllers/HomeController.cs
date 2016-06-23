using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Models;
using Transpo.WebApp.Models;

namespace Transpo.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new SearchResultModel();
            ViewData["Rides"] = model.Rides;
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //public void CreateAuthenticationTicket(long facebookId)
        //{
        //    var authUser = _userService.GetUserByFacebookId(facebookId);
        //    CustomPrincipalSerializedModel serializeModel = new CustomPrincipalSerializedModel();

        //    serializeModel.UserId = authUser.id;
        //    serializeModel.Name = authUser.Name;
        //    serializeModel.FacebookId = authUser.FacebookId;
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    string userData = serializer.Serialize(serializeModel);

        //    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
        //      1, authUser.Name, DateTime.Now, DateTime.Now.AddHours(2), false, userData);
        //    string encTicket = FormsAuthentication.Encrypt(authTicket);
        //    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        //    Response.Cookies.Add(faCookie);
        //}
    }
}