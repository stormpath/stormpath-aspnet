using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PreLogout
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/logout/aspnet/prelogout_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PreLogoutHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/logout/prelogout_handler_method.cs
        private Task MyPreLogoutHandler(
            PreLogoutContext context,
            CancellationToken ct)
        {
            return Task.FromResult(0);
        }
        #endregion
    }
}

