using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;
using Transpo.API.Models;
using Transpo.AppServices.DTOs;
using Transpo.AppServices.Models;
using Transpo.WebApp.Models;

namespace Transpo.API.Controllers
{
    public class HomeController : BaseApiController
    {
        //// GET: Home
        //public IHttpActionResult Index(int id = 0)
        //{
        //    var model = new SearchResultModel();
        //    ViewData["Rides"] = model.Rides;
        //    ViewBag.Login = id == 1;

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var userId = UserManager.FindById(User.Identity.GetUserId()).User.id;
        //        ViewBag.UnreadMessages = Service.GetMessageService().GetUnreadMessagesCount(userId);
        //    }

        //    return View();
        //}


        //[HttpPost]
        //public IHttpActionResult LogOff()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Home");
        //}

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