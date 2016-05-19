using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{

    public class Startup_Login
    {
        public void Configuration_Login(IAppBuilder app)
        {
            #region code/login/aspnet/configure_uri.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Login = new WebLoginRouteConfiguration
                    {
                        Uri = "/logMeIn"
                    }
                }
            });
            #endregion
        }

        public void Configuration_LoginChangeLabel(IAppBuilder app)
        {
            #region code/login/aspnet/configure_labels.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Login = new WebLoginRouteConfiguration
                    {
                        Form = new WebLoginRouteFormConfiguration
                        {
                            Fields = new Dictionary<string, WebFieldConfiguration>
                            {
                                ["login"] = new WebFieldConfiguration
                                {
                                    Label = "Email",
                                    Placeholder = "you@yourdomain.com"
                                },
                                ["password"] = new WebFieldConfiguration
                                {
                                    Placeholder = "Tip: Use a strong password!"
                                }
                            }
                        }
                    }
                }
            });
            #endregion
        }
    }
}
