using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stormpath.SDK.Account;
using Stormpath.SDK;

namespace Stormpath.AspNet
{
    public class RequiredCustomDataFilter : IAuthorizationFilter
    {
        private readonly string key;
        private readonly object value;

        public RequiredCustomDataFilter(string key, object value)
        {
            this.key = key;
            this.value = value;
        }

        public async Task<bool> IsAuthorized(IAccount account)
        {
            if (account == null)
            {
                return false;
            }

            var customData = await account.GetCustomDataAsync();

            if (customData?[key] == null)
            {
                return false;
            }

            if (!customData[key].Equals(value))
            {
                return false;
            }

            return true;
        }
    }
}
