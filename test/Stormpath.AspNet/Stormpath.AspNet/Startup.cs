using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;

[assembly: OwinStartup(typeof(Stormpath.AspNet.Startup))]

namespace Stormpath.AspNet
{
    // This should actually be Startup.Auth
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStormpath(new
            {
                application = new
                {
                    name = "My Application"
                }
            });

            app.Use<StormpathAuthenticationMiddleware>(new StormpathAuthenticationOptions("Cookie"));
            //app.UseMiddleware<StormpathAuthenticationMiddleware>(new StormpathAuthenticationOptions() { AuthenticationScheme = "Bearer" });

            //app.UseStageMarkre(PipelineStage.Authenticate);
        }
    }
}
