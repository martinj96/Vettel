using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Transpo.AppServices.Models;

namespace Transpo.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
        //    if (authCookie != null)
        //    {
        //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
        //        JavaScriptSerializer serializer = new JavaScriptSerializer();
        //        if (authTicket.UserData == "OAuth") return;
        //        CustomPrincipalSerializedModel serializeModel =
        //          serializer.Deserialize<CustomPrincipalSerializedModel>(authTicket.UserData);
        //        CustomPrincipal newUser = new CustomPrincipal(serializeModel.UserId, serializeModel.FacebookId,
        //            serializeModel.Name);
        //        HttpContext.Current.User = newUser;
        //    }
        //}
    }

}
