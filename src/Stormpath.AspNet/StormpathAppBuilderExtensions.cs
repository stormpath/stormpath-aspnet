﻿using System;
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

            var viewRenderer = new CompositeViewRenderer(options?.Logger,
                new PrecompiledViewRenderer(options?.Logger),
                new RazorViewRenderer());

            var stormpathMiddleware = StormpathMiddleware.Create(new StormpathOwinOptions()
            {
                LibraryUserAgent = GetLibraryUserAgent(),
                ViewRenderer = viewRenderer,
                Configuration = options?.Configuration,
                Logger = options?.Logger,
            });

            app.Use(stormpathMiddleware);

            app.Use<StormpathAuthenticationMiddleware>(new StormpathAuthenticationOptions("Cookie"), options?.Logger);
            app.Use<StormpathAuthenticationMiddleware>(new StormpathAuthenticationOptions("Bearer"), options?.Logger);

            app.UseStageMarker(PipelineStage.Authenticate);

            return app;
        }

        private static string GetLibraryUserAgent()
        {
            var libraryVersion = typeof(StormpathMiddleware).Assembly.GetName().Version;
            var libraryToken = $"stormpath-aspnet/{libraryVersion.Major}.{libraryVersion.Minor}.{libraryVersion.Build}";

            var hostToken = $"aspnet/{Environment.Version}";

            return string.Join(" ", libraryToken, hostToken);
        }
    }
}
