using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Transpo.AppServices;
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
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (authTicket.UserData == "OAuth") return;
                CustomPrincipalSerializedModel serializeModel =
                  serializer.Deserialize<CustomPrincipalSerializedModel>(authTicket.UserData);
                CustomPrincipal newUser = new CustomPrincipal(serializeModel.UserId, serializeModel.Name);
                HttpContext.Current.User = newUser;
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if (ex.GetType() == typeof(HttpException))
            {
                // handle http errors
            }

#if !DEBUG
            var emailService = new EmailService();
            emailService.SendEmail(new AppServices.DTOs.EmailDto
            {
                Body = ex.ToString(),
                FromEmail = "exceptions@kinisaj.mk",
                Subject = "Kinisaj.mk: Exception occurred",
                ToEmail = "exceptions@kinisaj.mk"
            });
#endif
            Server.ClearError();
        }
    }

}
