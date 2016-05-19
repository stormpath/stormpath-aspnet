using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_Oauth2
    {
        public void Configuration_Oauth2UriAndStrategy(IAppBuilder app)
        {
            #region code/oauth2/aspnet/configure_uri_and_strategy.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Oauth2 = new WebOauth2RouteConfiguration
                    {
                        Uri = "/api/token",
                        Password = new WebOauth2PasswordGrantConfiguration
                        {
                            ValidationStrategy = WebOauth2TokenValidationStrategy.Stormpath
                        }
                    }
                }
            });
            #endregion
        }
    }
}
