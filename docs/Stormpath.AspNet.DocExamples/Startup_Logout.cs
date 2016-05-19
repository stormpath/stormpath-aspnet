using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{

    public class Startup_Logout
    {
        public void Configuration_LogoutUris(IAppBuilder app)
        {
            #region code/logout/aspnet/configure_uris.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Logout = new WebLogoutRouteConfiguration
                    {
                        Uri = "/logMeOut",
                        NextUri = "/goodbye"
                    }
                }
            });
            #endregion
        }
    }
}
