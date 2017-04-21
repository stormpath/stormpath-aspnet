using System.Collections.Generic;
using System.Security.Claims;
using Stormpath.Owin.Abstractions;

namespace Stormpath.AspNet
{
    public static class AccountIdentityTransformer
    {
        public static ClaimsPrincipal CreatePrincipal(ICompatibleOktaAccount account, string scheme)
        {
            var identity = CreateIdentity(account, scheme);

            return identity == null 
                ? null 
                : new ClaimsPrincipal(identity);
        }

        public static ClaimsIdentity CreateIdentity(ICompatibleOktaAccount account, string scheme)
        {
            if (account == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Href),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.GivenName, account.GivenName),
                new Claim(ClaimTypes.Surname, account.Surname),
                new Claim("FullName", account.FullName)
            };

            var identity = new ClaimsIdentity(claims, scheme);

            return identity;
        }
    }
}
