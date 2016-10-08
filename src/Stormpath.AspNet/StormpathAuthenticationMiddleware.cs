using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Stormpath.Owin.Abstractions.Configuration;
using Stormpath.SDK.Client;
using Stormpath.SDK.Logging;

namespace Stormpath.AspNet
{
    public class StormpathAuthenticationMiddleware : AuthenticationMiddleware<StormpathAuthenticationOptions>
    {
        private readonly IClient _client; // TODO remove when refactoring JWT stuff
        private readonly IntegrationConfiguration _configuration;
        private readonly ILogger _stormpathLogger;

        public StormpathAuthenticationMiddleware(
            OwinMiddleware next,
            IClient client,
            StormpathAuthenticationOptions options,
            IntegrationConfiguration configuration,
            ILogger logger)
            : base(next, options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _client = client;
            _configuration = configuration;
            _stormpathLogger = logger;
        }

        // Called for each request, to create a handler for each request.
        protected override AuthenticationHandler<StormpathAuthenticationOptions> CreateHandler()
        {
            return new StormpathAuthenticationHandler(
                _client,
                _configuration,
                _stormpathLogger);
        }
    }
}
