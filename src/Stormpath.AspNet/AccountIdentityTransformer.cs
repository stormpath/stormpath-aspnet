using Stormpath.SDK.Account;
using Stormpath.SDK.Sync;
using System.Collections.Generic;
using System.Security.Claims;

namespace Stormpath.AspNet
{
    public static class AccountIdentityTransformer
    {
        public static ClaimsPrincipal CreatePrincipal(IAccount account, string scheme)
        {
            var identity = CreateIdentity(account, scheme);

            return identity == null
                ? null
                : new ClaimsPrincipal(identity);
        }

        public static ClaimsIdentity CreateIdentity(IAccount account, string scheme)
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

            //var groups = account.GetGroups().Synchronously();

            //foreach (var group in groups)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, group.Name));
            //}

            var identity = new ClaimsIdentity(claims, scheme);

            return identity;
        }
    }
}
