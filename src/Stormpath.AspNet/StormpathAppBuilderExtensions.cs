using System;
using System.Text;
using Owin;
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

            app.Use<StormpathAuthenticationMiddleware>(new StormpathAuthenticationOptions("Cookie"));
            app.Use<StormpathAuthenticationMiddleware>(new StormpathAuthenticationOptions("Bearer"));

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
}
