using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PreLogin
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/login/aspnet/prelogin_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PreLoginHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/login/prelogin_handler_method.cs
        private Task MyPreLoginHandler(
            PreLoginContext context,
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
                #region code/login/prelogin_target_dir.cs
                PreLoginHandler = async (ctx, ct) =>
                {
                    ctx.AccountStore = await ctx.Client.GetDirectoryAsync(
                        "https://api.stormpath.com/v1/directories/xxx", ct);
                }
                #endregion
            });
        }
    }
}

