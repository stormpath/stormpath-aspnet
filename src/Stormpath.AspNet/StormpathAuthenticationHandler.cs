using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Stormpath.Configuration.Abstractions.Immutable;
using Stormpath.Owin.Abstractions;
using Stormpath.Owin.Abstractions.Configuration;
using Stormpath.Owin.Middleware;
using Stormpath.SDK.Account;
using Stormpath.SDK.Logging;

namespace Stormpath.AspNet
{
    public sealed class StormpathAuthenticationHandler : AuthenticationHandler<StormpathAuthenticationOptions>
    {
        private readonly SDK.Logging.ILogger _stormpathLogger;
        private readonly RouteProtector _protector;

        public StormpathAuthenticationHandler(IntegrationConfiguration configuration, SDK.Logging.ILogger stormpathLogger)
        {
            _stormpathLogger = stormpathLogger;

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
            var redirectAction = new Action<string>(location => Response.Redirect(location));

            return new RouteProtector(
                configuration.Web,
                deleteCookieAction,
                setStatusCodeAction,
                redirectAction,
                _stormpathLogger);
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var scheme = Context.Get<string>(OwinKeys.StormpathUserScheme);
            var account = Context.Get<IAccount>(OwinKeys.StormpathUser);

            if (Options.AllowedAuthenticationSchemes.Any(potentialScheme => _protector.IsAuthenticated(scheme, potentialScheme, account)))
            {
                var principal = AccountIdentityTransformer.CreateIdentity(account, scheme);
                var ticket = new AuthenticationTicket(principal, new AuthenticationProperties());
                return Task.FromResult(ticket);
            }

            _stormpathLogger.Trace("Request is not authenticated", source: nameof(StormpathAuthenticationHandler));
            return Task.FromResult<AuthenticationTicket>(null);
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401 || Response.StatusCode != 403)
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
