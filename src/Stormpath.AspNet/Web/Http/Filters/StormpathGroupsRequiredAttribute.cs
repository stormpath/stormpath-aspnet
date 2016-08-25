using Stormpath.SDK;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Stormpath.AspNet.Web.Http.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class StormpathGroupsRequiredAttribute : Attribute, IAuthenticationFilter
    {
        private readonly string[] allowedGroupNames;

        public StormpathGroupsRequiredAttribute(params string[] allowedGroupNames)
        {
            this.allowedGroupNames = allowedGroupNames;
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (!context.Principal.Identity.IsAuthenticated)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            var account = context.Request.GetStormpathAccount();
            if (account == null)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            var groupNames = (await account
                .GetGroups()
                .ToListAsync())
                .Select(g => g.Name)
                .ToList();

            var matchingGroupFound = false;

            foreach (var name in allowedGroupNames)
            {
                matchingGroupFound = groupNames.Contains(name, StringComparer.OrdinalIgnoreCase);

                if (matchingGroupFound)
                {
                    break;
                }
            }

            if (!matchingGroupFound)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            // At least one group matches; okay to continue!
            // This functionality matches the [Authorize] attribute.  Use multiple [StormpathGroupsRequired] attributes to create "and" conditions
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public bool AllowMultiple => true;

        public override object TypeId => this;
    }
}
