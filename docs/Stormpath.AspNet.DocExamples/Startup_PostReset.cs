using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PostReset
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/password_reset/aspnet/postreset_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PostChangePasswordHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/password_reset/postreset_handler_method.cs
        private Task MyPostResetHandler(
            PostChangePasswordContext context,
            CancellationToken ct)
        {
            return Task.FromResult(0);
        }
        #endregion
    }
}

