using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PreRegistration
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/registration/aspnet/preregistration_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PreRegistrationHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/registration/preregistration_handler_method.cs
        private Task MyPreRegistrationHandler(
            PreRegistrationContext context,
            CancellationToken ct)
        {
            return Task.FromResult(0);
        }
        #endregion

        public void Configuration_TargetDirectory(IAppBuilder app)
        {
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                #region code/registration/preregistration_target_dir.cs
                PreRegistrationHandler = async (ctx, ct) =>
                {
                    ctx.AccountStore = await ctx.Client.GetDirectoryAsync(
                        "https://api.stormpath.com/v1/directories/xxx", ct);
                }
                #endregion
            });
        }
    }
}

