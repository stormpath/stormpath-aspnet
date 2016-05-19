using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_Me
    {
        public void Configuration_MeUri(IAppBuilder app)
        {
            #region code/me_api/aspnet/configure_uri.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Me = new WebMeRouteConfiguration
                    {
                        Uri = "/userDetails"
                    }
                }
            });
            #endregion
        }
    }
}
