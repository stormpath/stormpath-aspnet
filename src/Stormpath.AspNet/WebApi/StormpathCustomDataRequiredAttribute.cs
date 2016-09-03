using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.WebApi
{
    /// <summary>
    /// Requires a matching Stormpath Custom Data element to be present for the user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class StormpathCustomDataRequiredAttribute : Attribute, IAuthenticationFilter
    {
        private readonly string _key;
        private readonly object _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StormpathCustomDataRequiredAttribute"/> class.
        /// </summary>
        /// <param name="key">The Custom Data key.</param>
        /// <param name="value">The Custom Data value.</param>
        public StormpathCustomDataRequiredAttribute(string key, object value)
        {
            _key = key;
            _value = value;
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (!context.Principal.Identity.IsAuthenticated)
            {
                context.ErrorResult = new AuthorizationFailureResult(HttpStatusCode.Unauthorized, "Not authenticated", context.Request);
                return;
            }

            var account = context.Request.GetStormpathAccount();

            var requireCustomDataFilter = new RequireCustomDataFilter(_key, _value);
            var authorized = await requireCustomDataFilter.IsAuthorizedAsync(account, cancellationToken);

            if (!authorized)
            {
                context.ErrorResult = new AuthorizationFailureResult(HttpStatusCode.Forbidden, "Not Authorized", context.Request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
            => Task.FromResult(0);

        public bool AllowMultiple => true;

        public override object TypeId => this;
    }
}
