using System;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Stormpath.Owin.Abstractions.Configuration;

namespace Stormpath.AspNet
{
    public class StormpathAuthenticationMiddleware : AuthenticationMiddleware<StormpathAuthenticationOptions>
    {
        private readonly IntegrationConfiguration _configuration;
        private readonly ILogger _logger;

        public StormpathAuthenticationMiddleware(
            OwinMiddleware next,
            StormpathAuthenticationOptions options,
            IntegrationConfiguration configuration,
            ILogger logger)
            : base(next, options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _configuration = configuration;
            _logger = logger;
        }

        // Called for each request, to create a handler for each request.
        protected override AuthenticationHandler<StormpathAuthenticationOptions> CreateHandler()
        {
            return new StormpathAuthenticationHandler(_configuration, _logger);
        }
    }
}
