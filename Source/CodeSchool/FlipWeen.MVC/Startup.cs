using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlipWeen.MVC.Startup))]
namespace FlipWeen.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
