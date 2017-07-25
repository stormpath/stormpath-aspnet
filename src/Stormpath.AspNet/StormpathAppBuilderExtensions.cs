using System;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Owin.Extensions;
using Owin;
using Stormpath.Configuration.Abstractions;
using Stormpath.Owin.Middleware;
using Stormpath.Owin.Views.Precompiled;

namespace Stormpath.AspNet
{
    public static class StormpathAppBuilderExtensions
    {
        public static IAppBuilder UseStormpath(this IAppBuilder app)
            => app.UseStormpath(new StormpathMiddlewareOptions());

        public static IAppBuilder UseStormpath(this IAppBuilder app, StormpathConfiguration configuration)
            => app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = configuration
            });

        public static IAppBuilder UseStormpath(this IAppBuilder app, object anonymousConfiguration)
            => app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = anonymousConfiguration
            });

        public static IAppBuilder UseStormpath(this IAppBuilder app, StormpathMiddlewareOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var safeLogger = options?.Logger ?? NullLogger.Instance;

            var viewRenderer = new CompositeViewRenderer(safeLogger,
                new PrecompiledViewRenderer(safeLogger),
                new RazorViewRenderer(safeLogger));

            var stormpathMiddleware = StormpathMiddleware.Create(new StormpathOwinOptions()
            {
                LibraryUserAgent = GetLibraryUserAgent(),
                ViewRenderer = viewRenderer,
                Configuration = options?.Configuration,
                ConfigurationFileRoot = AppDomain.CurrentDomain.BaseDirectory,
                Logger = safeLogger,
                PostChangePasswordHandler = options?.PostChangePasswordHandler,
                PostLoginHandler = options?.PostLoginHandler,
                PostLogoutHandler = options?.PostLogoutHandler,
                PostRegistrationHandler = options?.PostRegistrationHandler,
                PostVerifyEmailHandler = options?.PostVerifyEmailHandler,
                PreChangePasswordHandler = options?.PreChangePasswordHandler,
                PreLoginHandler = options?.PreLoginHandler,
                PreLogoutHandler = options?.PreLogoutHandler,
                PreRegistrationHandler = options?.PreRegistrationHandler,
                PreVerifyEmailHandler = options?.PreVerifyEmailHandler,
                SendVerificationEmailHandler = options?.SendVerificationEmailHandler
            });

            app.Use(stormpathMiddleware);

            app.Use<StormpathAuthenticationMiddleware>(
                new StormpathAuthenticationOptions() { AllowedAuthenticationSchemes = new[] { "Cookie", "Bearer" } },
                stormpathMiddleware.Configuration,
                safeLogger);

            app.UseStageMarker(PipelineStage.Authenticate);

            return app;
        }

        private static string GetLibraryUserAgent()
        {
            var libraryVersion = typeof(StormpathAuthenticationMiddleware).Assembly.GetName().Version;
            var libraryToken = $"stormpath-aspnet/{libraryVersion.Major}.{libraryVersion.Minor}.{libraryVersion.Build}";

            var hostToken = $"aspnet/{Environment.Version}";

            return string.Join(" ", libraryToken, hostToken);
        }
    }
}
