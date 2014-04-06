using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebClient_.Startup))]
namespace WebClient_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
