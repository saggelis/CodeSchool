using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeSchool.Startup))]
namespace CodeSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
