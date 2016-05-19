using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PasswordReset
    {
        public void Configuation_PasswordResetUris(IAppBuilder app)
        {
            #region code/password_reset/aspnet/configure_uris.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    ForgotPassword = new WebForgotPasswordRouteConfiguration
                    {
                        Uri = "/forgot-password"
                    },
                    ChangePassword = new WebChangePasswordRouteConfiguration
                    {
                        Uri = "/change-password"
                    }
                }
            });
            #endregion
        }
    }
}
