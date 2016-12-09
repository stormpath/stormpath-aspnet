using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PreVerify
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/email_verification/aspnet/preverify_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PreVerifyEmailHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/email_verification/preverify_handler_method.cs
        private Task MyPreVerifyHandler(
            PreVerifyEmailContext context,
            CancellationToken ct)
        {
            return Task.FromResult(0);
        }
        #endregion
    }
}

