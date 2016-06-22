using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Transpo.WebApp.Startup))]
namespace Transpo.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
