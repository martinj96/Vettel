using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Transpo.Infrastructure.Data;
using Transpo.Infrastructure.Data.Identity;

namespace Transpo.WebApp
{
    public partial class Startup
    {
        public string XmlSchemaString { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            app.CreatePerOwinContext(() => new TranspoDbContext(connectionString));
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);
            //app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
            //    new RoleManager<AppRole>(
            //        new RoleStore<AppRole>(context.Get<TranspoDbContext>())));

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login")
            });

            var facebookAuthOptions = new FacebookAuthenticationOptions
            {
                AppId = ConfigurationManager.AppSettings["FacebookAppId"].ToString(),
                AppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"].ToString(),
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("FacebookAccessToken", context.AccessToken));
                        foreach (var claim in context.User)
                        {
                            var claimType = string.Format("urn:facebook:{0}", claim.Key);
                            string claimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, claimValue))
                                context.Identity.AddClaim(new Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));
                        }

                        return Task.FromResult(0);
                    }
                }
            };
            facebookAuthOptions.Scope.Add("email");

            app.UseFacebookAuthentication(facebookAuthOptions);
        }
    }
}