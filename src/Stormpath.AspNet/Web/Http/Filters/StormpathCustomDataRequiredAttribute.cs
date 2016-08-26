using Stormpath.SDK;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Stormpath.AspNet.Web.Http.Filters
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
            if (account == null)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            var customData = await account.GetCustomDataAsync();
            if (customData == null)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            if (customData[key] == null)
            {
                context.ErrorResult = new AuthorizationFailureResult("Not Authorized", context.Request);
                return;
            }

            if (!customData[key].Equals(value))
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
