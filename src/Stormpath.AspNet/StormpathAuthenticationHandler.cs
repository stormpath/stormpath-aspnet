using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Stormpath.Configuration.Abstractions.Immutable;
using Stormpath.Owin.Abstractions;
using Stormpath.Owin.Abstractions.Configuration;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet
{
    public sealed class StormpathAuthenticationHandler : AuthenticationHandler<StormpathAuthenticationOptions>
    {
        private readonly ILogger _logger;
        private readonly RouteProtector _protector;

        public StormpathAuthenticationHandler(
            IntegrationConfiguration configuration,
            ILogger logger)
        {
            _logger = logger ?? NullLogger.Instance;

            _protector = CreateRouteProtector(configuration);
        }

        private RouteProtector CreateRouteProtector(IntegrationConfiguration configuration)
        {
            var deleteCookieAction = new Action<WebCookieConfiguration>(cookie =>
            {
                Response.Cookies.Delete(cookie.Name, new CookieOptions()
                {
                    Domain = cookie.Domain,
                    Path = cookie.Path
                });
            });

            var setStatusCodeAction = new Action<int>(code => Response.StatusCode = code);
            var setHeaderAction = new Action<string, string>((name, value) => Response.Headers.Set(name, value));
            var redirectAction = new Action<string>(location => Response.Redirect(location));

            return new RouteProtector(
                configuration,
                deleteCookieAction,
                setStatusCodeAction,
                setHeaderAction,
                redirectAction,
                _logger);
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var scheme = Context.Get<string>(OwinKeys.StormpathUserScheme);
            var account = Context.Get<ICompatibleOktaAccount>(OwinKeys.StormpathUser);

            if (Options.AllowedAuthenticationSchemes.Any(potentialScheme => _protector.IsAuthenticated(scheme, potentialScheme, account)))
            {
                var principal = AccountIdentityTransformer.CreateIdentity(account, scheme);
                var ticket = new AuthenticationTicket(principal, new AuthenticationProperties());
                return Task.FromResult(ticket);
            }

            _logger.LogTrace("Request is not authenticated", nameof(StormpathAuthenticationHandler));
            return Task.FromResult<AuthenticationTicket>(null);
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401 && Response.StatusCode != 403)
            {
                return Task.FromResult<object>(null);
            }

            var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);
            if (challenge == null)
            {
                return Task.FromResult<object>(null);
            }

            // Redirects and deletes cookies as necessary
            _protector.OnUnauthorized(Request.Headers["Accept"], Request.Path.ToString());

            return Task.FromResult<object>(null);
        }
    }
}
