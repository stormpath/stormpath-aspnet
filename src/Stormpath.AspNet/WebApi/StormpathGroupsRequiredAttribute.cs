using Stormpath.SDK;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Stormpath.AspNet.WebApi
{
    /// <summary>
    /// The functionality of this attribute matches the [Authorize] attribute.  
    /// You can put multiple groups in a single call.  They will be treated like an "or".  
    /// If the user matches one of the groups listed, the call will be allowed.
    /// Use multiple [StormpathGroupsRequired] attributes to create "and" conditions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class StormpathGroupsRequiredAttribute : Attribute, IAuthenticationFilter
    {
        private readonly string[] allowedGroupNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="StormpathGroupsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="allowedGroupNames"></param>
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
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public bool AllowMultiple => true;

        public override object TypeId => this;
    }
}
