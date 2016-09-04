using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Transpo.API.Models;
using Transpo.Infrastructure.Data.Identity;

namespace Transpo.API.Filters
{
    public class BasicAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
                AppUser user = null;

                if (username == "acccess_token")
                {
                    var loginInfo = HttpContext.Current.GetOwinContext().Authentication.GetExternalLoginInfo();

                    user = userManager.FindByEmail(loginInfo.Email);
                }
                else
                {
                    string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                    PasswordHasher hasher = new PasswordHasher();

                    user = userManager.Find(username, password);
                }

                if (user != null)
                {
                    HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user.User), new string[] { });
                    base.OnActionExecuting(actionContext);
                }
                else
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }

        }
    }
}