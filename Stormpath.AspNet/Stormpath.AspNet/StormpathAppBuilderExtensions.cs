using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Owin;
using Stormpath.Owin.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet
{
    public static class StormpathAppBuilderExtensions
    {
        public static IAppBuilder UseStormpath(this IAppBuilder app, object configuration = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var stormpathMiddleware = StormpathMiddleware.Create(new StormpathMiddlewareOptions()
            {
                LibraryUserAgent = GetLibraryUserAgent(app),
                Configuration = configuration,
                ViewRenderer = new SimpleViewRenderer()
            });

            app.Use(stormpathMiddleware);

            return app;
        }

        private static string GetLibraryUserAgent(IAppBuilder app)
        {
            var builder = new StringBuilder();

            builder.Append($"aspnet/{Environment.Version.ToString()}");
            // todo append MVC package version

            return builder.ToString();
        }
    }

    public class SimpleViewRenderer : IViewRenderer
    {
        public Task RenderAsync(string viewName, object viewModel, IOwinEnvironment context, CancellationToken cancellationToken)
        {
            var view = Stormpath.Owin.Views.Precompiled.ViewResolver.GetView(viewName);
            if (view == null)
            {
                // Or, hook into your existing view rendering implementation
                throw new Exception($"View '{viewName}' not found.");
            }

            return view.ExecuteAsync(viewModel, context.Response.Body);
        }
    }
}
