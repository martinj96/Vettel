using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
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

namespace Transpo.API
{
    public partial class Startup
    {
        public string XmlSchemaString { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            string connectionString = DAUtilities.ConnectionString;
            app.CreatePerOwinContext(() => new TranspoDbContext(connectionString));
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);
        }
    }
}
