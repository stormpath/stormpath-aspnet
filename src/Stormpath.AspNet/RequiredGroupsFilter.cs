using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stormpath.SDK.Account;
using Stormpath.SDK;

namespace Stormpath.AspNet
{
    public class RequiredGroupsFilter : IAuthorizationFilter
    {
        private readonly string[] anyAllowedGroups;

        public RequiredGroupsFilter(string[] anyAllowedGroups)
        {
            this.anyAllowedGroups = anyAllowedGroups;
        }

        public async Task<bool> IsAuthorized(IAccount account)
        {
            // the group lookup and matching logic here
            if (account == null)
            {
                return false;
            }

            var groupNames = (await account
                .GetGroups()
                .ToListAsync())
                .Select(g => g.Name)
                .ToList();

            //var groupNames = GetGroupNames(account);

            var matchingGroupFound = false;

            foreach (var name in anyAllowedGroups)
            {
                matchingGroupFound = groupNames.Contains(name, StringComparer.OrdinalIgnoreCase);

                if (matchingGroupFound)
                {
                    break;
                }
            }

            if (!matchingGroupFound)
            {
                return false;
            }

            return true;
        }
    }
}
