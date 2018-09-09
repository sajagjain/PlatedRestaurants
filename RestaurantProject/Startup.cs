using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestaurantProject.Startup))]
namespace RestaurantProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
