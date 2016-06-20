using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Stormpath.Configuration.Abstractions.Immutable;
using Stormpath.Owin.Abstractions;
using Stormpath.Owin.Middleware;
using Stormpath.SDK.Account;

namespace Stormpath.AspNet
{
    public sealed class StormpathAuthenticationHandler : AuthenticationHandler<StormpathAuthenticationOptions>
    {
        private readonly SDK.Logging.ILogger stormpathLogger;
        private RouteProtector handler;

        public StormpathAuthenticationHandler(SDK.Logging.ILogger stormpathLogger)
        {
            this.stormpathLogger = stormpathLogger;
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var config = Context.Get<StormpathConfiguration>(OwinKeys.StormpathConfiguration);
            var scheme = Context.Get<string>(OwinKeys.StormpathUserScheme);
            var account = Context.Get<IAccount>(OwinKeys.StormpathUser);

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

            this.handler = new RouteProtector(
                config.Web,
                deleteCookieAction,
                setStatusCodeAction,
                redirectAction,
                this.stormpathLogger);

            if (!handler.IsAuthenticated(scheme, Options.AuthenticationType, account))
            {
                return Task.FromResult<AuthenticationTicket>(null);
            }
                
            var principal = AccountIdentityTransformer.CreateIdentity(account, scheme);
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties());
            return Task.FromResult(ticket);
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode != 401)
            {
                return Task.FromResult<object>(null);
            }

            var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);
            if (challenge == null)
            {
                return Task.FromResult<object>(null);
            }

            if (this.handler != null)
            {
                // Redirects and deletes cookies as necessary
                handler.OnUnauthorized(Request.Headers["Accept"], Request.Path.ToString());
            }

            return Task.FromResult<object>(null);
        }
    }
}
