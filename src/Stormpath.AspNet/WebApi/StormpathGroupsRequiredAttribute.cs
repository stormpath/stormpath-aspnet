using Stormpath.SDK;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.WebApi
{
    /// <summary>
    /// Requires the user to be in a Stormpath Group.
    /// </summary>
    /// <remarks>
    /// The functionality of this attribute matches <c>AuthorizeAttribute</c>.
    /// Multiple Groups in a single attribute are treated as an OR.
    /// Use multiple <c>StormpathGroupsRequiredAttribute</c> to create AND conditions.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class StormpathGroupsRequiredAttribute : Attribute, IAuthenticationFilter
    {
        private readonly string[] _allowedGroupNamesOrHrefs;

        /// <summary>
        /// Initializes a new instance of the <see cref="StormpathGroupsRequiredAttribute"/> class.
        /// </summary>
        /// <param name="allowedNamesorHrefs">The case-sensitive name or <c>href</c> of a Stormpath Group.</param>
        public StormpathGroupsRequiredAttribute(params string[] allowedNamesorHrefs)
        {
            _allowedGroupNamesOrHrefs = allowedNamesorHrefs;
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (!context.Principal.Identity.IsAuthenticated)
            {
                context.ErrorResult = new AuthorizationFailureResult(HttpStatusCode.Unauthorized, "Not authenticated", context.Request);
                return;
            }

            var account = context.Request.GetStormpathAccount();

            var requireGroupsFilter = new RequireGroupsFilter(_allowedGroupNamesOrHrefs);
            var authorized = await requireGroupsFilter.IsAuthorizedAsync(account, cancellationToken);

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
