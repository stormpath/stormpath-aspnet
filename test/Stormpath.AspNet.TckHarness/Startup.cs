using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Stormpath.AspNet.TckHarness.Startup))]
namespace Stormpath.AspNet.TckHarness
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
