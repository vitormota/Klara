using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DumpTester.Startup))]
namespace DumpTester
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
