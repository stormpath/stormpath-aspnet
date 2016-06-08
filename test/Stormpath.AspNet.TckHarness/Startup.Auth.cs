using System;
using System.IO;
using System.Text;
using Owin;
using Stormpath.SDK.Logging;

namespace Stormpath.AspNet.TckHarness
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var stormpathConfig = new
            {
                application = new
                {
                    name = "My Application"
                }
            };

            var logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StormpathMiddleware.log");

            app.UseStormpath(new StormpathMiddlewareOptions()
            {
                Configuration = stormpathConfig,
                Logger = new FileLogger(logPath, LogLevel.Trace)
            });
        }
    }

    public class FileLogger : ILogger
    {
        private readonly string _path;
        private readonly LogLevel _severity;

        public FileLogger(string path, LogLevel severity)
        {
            _path = path;
            _severity = severity;
        }

        public void Log(LogEntry entry)
        {
            if (entry.Severity < _severity)
            {
                return;
            }

            var logBuilder = new StringBuilder()
                .Append($"[{entry.Severity}] {entry.Source}: ");

            if (entry.Exception != null)
            {
                logBuilder.Append($"Exception {entry.Exception.GetType().Name} \"{entry.Exception.Message}\" in {entry.Exception.Source} ");
            }

            bool isMessageUseful = !string.IsNullOrEmpty(entry.Message)
                                   && !entry.Message.Equals(entry.Exception?.Message, StringComparison.Ordinal);
            if (isMessageUseful)
            {
                logBuilder.Append($"\"{entry.Message}\"");
            }

            logBuilder.Append("\n");

            File.AppendAllText(_path, logBuilder.ToString());
        }
    }
}

