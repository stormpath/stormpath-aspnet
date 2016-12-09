using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PostLogin
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/login/aspnet/postlogin_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PostLoginHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/login/postlogin_handler_method.cs
        private Task MyPostLoginHandler(
            PostLoginContext context,
            CancellationToken ct)
        {
            return Task.FromResult(0);
        }
        #endregion

        public void Configuration_UpdateCustomData(IAppBuilder app)
        {
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                #region code/login/postlogin_save_time.cs
                PostLoginHandler = async (ctx, ct) =>
                {
                    ctx.Account.CustomData["lastSeen"] = DateTimeOffset.UtcNow;
                    await ctx.Account.SaveAsync(ct);
                }
                #endregion
            });
        }
    }
}

