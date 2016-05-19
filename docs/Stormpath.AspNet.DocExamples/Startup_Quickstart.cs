using System.Collections.Generic;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_Quickstart
    {
        #region code/quickstart/aspnet/configure.cs
        public void Configuration(IAppBuilder app)
        {
            app.UseStormpath();

            // MVC or other framework middleware here
        }
        #endregion
    }
}
