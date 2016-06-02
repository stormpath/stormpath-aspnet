using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;

namespace Stormpath.AspNet
{
    public class StormpathAuthenticationMiddleware : AuthenticationMiddleware<StormpathAuthenticationOptions>
    {
        private readonly SDK.Logging.ILogger stormpathLogger;

        public StormpathAuthenticationMiddleware(OwinMiddleware next, StormpathAuthenticationOptions options)
            : base(next, options)
        {
            this.stormpathLogger = null; // todo make logging available
        }

        // Called for each request, to create a handler for each request.
        protected override AuthenticationHandler<StormpathAuthenticationOptions> CreateHandler()
        {
            return new StormpathAuthenticationHandler(this.stormpathLogger);
        }
    }
}
