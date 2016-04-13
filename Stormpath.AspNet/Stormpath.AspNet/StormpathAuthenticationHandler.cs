using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private RouteProtector handler;

        public override async Task<bool> InvokeAsync()
        {
            var ticket = await AuthenticateAsync();

            if (ticket != null)
            {
                Context.Authentication.SignIn(ticket.Properties, ticket.Identity);

                // All good! Let the rest of the pipeline run
                return false;
            }
            else
            {
                return true; // Prevent further processing (return challenge)
            }
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
                redirectAction);

            if (this.handler.IsAuthenticated(scheme, Options.AuthenticationType, account))
            {
                var principal = CreatePrincipal(account, scheme);
                var ticket = new AuthenticationTicket(principal, new AuthenticationProperties());

                return Task.FromResult(ticket);
            }

            return Task.FromResult<AuthenticationTicket>(null);
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (this.handler != null)
            {
                // Redirects and deletes cookies as necessary
                handler.OnUnauthorized(Request.Headers["Accept"], Request.Path.ToString());
            }

            return Task.FromResult<object>(null);
        }

        private static ClaimsIdentity CreatePrincipal(IAccount account, string scheme)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, account.Href));
            claims.Add(new Claim(ClaimTypes.Email, account.Email));
            claims.Add(new Claim(ClaimTypes.Name, account.Username));
            claims.Add(new Claim(ClaimTypes.GivenName, account.GivenName));
            claims.Add(new Claim(ClaimTypes.Surname, account.Surname));
            claims.Add(new Claim("FullName", account.FullName));

            var identity = new ClaimsIdentity(claims, scheme);

            return identity;
        }
    }

}
