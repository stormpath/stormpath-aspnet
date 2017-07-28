using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Owin;
using Stormpath.Configuration.Abstractions;

namespace Stormpath.AspNet.TckHarness
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StormpathMiddleware.log");

            var stormpathConfiguration = new StormpathConfiguration
            {
                // Replace with your Okta Base URL:
                Org = "https://dev-123456.oktapreview.com",
                // Replace with your Okta API token:
                ApiToken = "your_api_token",
                Application = new OktaApplicationConfiguration
                {
                    // Replace with your Okta migrated Application ID:
                    Id = "abcd12345"
                },
                Web = new WebConfiguration
                {
                    ServerUri = "http://localhost:8080",
                    ChangePassword = new WebChangePasswordRouteConfiguration
                    {
                        Enabled = true
                    },
                    ForgotPassword = new WebForgotPasswordRouteConfiguration
                    {
                        Enabled = true
                    }
                }
            };

            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = stormpathConfiguration,
                Logger = new FileLogger(logPath, LogLevel.Trace),
                CacheProvider = new MemoryDistributedCache(new MemoryCache(new MemoryCacheOptions())),
            });
        }
    }

    public class FileLogger : ILogger
    {
        private readonly string _path;
        private readonly LogLevel _severity;
        private readonly object _lock = new object();

        public FileLogger(string path, LogLevel severity)
        {
            _path = path;
            _severity = severity;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel <= _severity;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var logBuilder = new StringBuilder()
                .Append($"[{logLevel}] {eventId}: ");

            var message = formatter(state, exception);
            logBuilder.AppendLine(message);

            lock (_lock)
            {
                File.AppendAllText(_path, logBuilder.ToString());
            }
        }
    }
}

