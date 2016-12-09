using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.DocExamples
{
    public class Startup_PostRegistration
    {
        public void Configuration_Basic(IAppBuilder app)
        {
            #region code/registration/aspnet/postregistration_handler.cs
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                PostRegistrationHandler = (context, ct) =>
                {
                    return Task.FromResult(0);
                }
            });
            #endregion
        }

        #region code/registration/postregistration_handler_method.cs
        private Task MyPostRegistrationHandler(
            PostRegistrationContext context,
            CancellationToken ct)
        {
            return Task.FromResult(0);
        }
        #endregion

        public void Configuration_SaveCustomData(IAppBuilder app)
        {
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                #region code/registration/postregistration_save_data.cs
                PostRegistrationHandler = async (ctx, ct) =>
                {
                    ctx.Account.CustomData["quota"] = 100;
                    await ctx.Account.SaveAsync(ct);
                }
                #endregion
            });
        }

        public void Configuration_AddGroup(IAppBuilder app)
        {
            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = new StormpathConfiguration(), // existing config, if any
                #region code/registration/postregistration_add_group.cs
                PostRegistrationHandler = async (ctx, ct) =>
                {
                    await ctx.Account.AddGroupAsync("rebels", ct);
                }
                #endregion
            });
        }
    }
}

