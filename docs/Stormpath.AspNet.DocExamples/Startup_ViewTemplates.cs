using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_ViewTemplates
    {
        public void Configuration_CustomView(IAppBuilder app)
        {
            #region code/templates/aspnet/custom_view.cs
            app.UseStormpath(new StormpathConfiguration
            {
                Web = new WebConfiguration
                {
                    Login = new WebLoginRouteConfiguration
                    {
                        View = "~/Views/Login/MyLogin.cshtml"
                    }
                }
            });
            #endregion
        }
    }
}
