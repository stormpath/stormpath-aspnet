using Stormpath.SDK;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Stormpath.AspNet.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class StormpathCustomDataRequiredAttribute : Attribute, IAuthenticationFilter
    {
        private readonly string key;
        private readonly object value;

        public StormpathCustomDataRequiredAttribute(string key, object value)
        {
            this.key = key;
            this.value = value;
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (!context.Principal.Identity.IsAuthenticated)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            var account = context.Request.GetStormpathAccount();

            var requiredCustomDataFilter = new RequiredCustomDataFilter(key, value);
            var authorized = await requiredCustomDataFilter.IsAuthorized(account);
            if (!authorized)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            // The Custom Data matches; okay to continue!
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public bool AllowMultiple => true;

        public override object TypeId => this;
    }
}
