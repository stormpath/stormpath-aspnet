using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PostLogout
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/logout/aspnet/postlogout_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PostLogoutHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/logout/postlogout_handler_method.cs
        private Task MyPostLogoutHandler(
            PostLogoutContext context,
            CancellationToken ct)
        {
            return Task.FromResult(0);
        }
        #endregion
    }
}

