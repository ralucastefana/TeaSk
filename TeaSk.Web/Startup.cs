using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeaSk.Web.Startup))]
namespace TeaSk.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
