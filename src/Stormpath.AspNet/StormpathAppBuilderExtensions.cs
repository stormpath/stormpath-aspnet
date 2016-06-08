using System;
using System.Text;
using Microsoft.Owin.Extensions;
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

            app.UseStageMarker(PipelineStage.Authenticate);

            return app;
        }

        private static string GetLibraryUserAgent(IAppBuilder app)
        {
            var libraryVersion = typeof(StormpathMiddleware).Assembly.GetName().Version;
            var libraryToken = $"stormpath-aspnet/{libraryVersion.Major}.{libraryVersion.Minor}.{libraryVersion.Build}";

            var hostToken = $"aspnet/{Environment.Version.ToString()}";

            return string.Join(" ", libraryToken, hostToken);
        }
    }
}
