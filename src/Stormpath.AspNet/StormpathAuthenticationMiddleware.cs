using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Stormpath.SDK.Logging;

namespace Stormpath.AspNet
{
    public class StormpathAuthenticationMiddleware : AuthenticationMiddleware<StormpathAuthenticationOptions>
    {
        private readonly ILogger _stormpathLogger;

        public StormpathAuthenticationMiddleware(OwinMiddleware next, StormpathAuthenticationOptions options, ILogger logger)
            : base(next, options)
        {
            _stormpathLogger = logger;
        }

        // Called for each request, to create a handler for each request.
        protected override AuthenticationHandler<StormpathAuthenticationOptions> CreateHandler()
        {
            return new StormpathAuthenticationHandler(_stormpathLogger);
        }
    }
}
