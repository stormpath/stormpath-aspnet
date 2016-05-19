using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{

    public class Startup_EmailVerification
    {
        public void Configuration_VerifyEmailUri(IAppBuilder app)
        {
            #region code/email_verification/aspnet/configure_uri.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    VerifyEmail = new WebVerifyEmailRouteConfiguration
                    {
                        Uri = "/verifyEmail"
                    }
                }
            });
            #endregion
        }
    }
}
