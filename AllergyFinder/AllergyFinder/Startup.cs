using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AllergyFinder.Startup))]
namespace AllergyFinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
