using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebInstitution.Startup))]
namespace WebInstitution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
