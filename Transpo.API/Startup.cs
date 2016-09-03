using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Transpo.API.Startup))]
namespace Transpo.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}