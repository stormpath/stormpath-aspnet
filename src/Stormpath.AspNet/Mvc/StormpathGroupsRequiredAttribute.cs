using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Stormpath.Owin.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.Mvc
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
    public class StormpathGroupsRequiredAttribute : ActionFilterAttribute, IAuthenticationFilter
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

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!filterContext.Principal.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }

            var account = filterContext.RequestContext.HttpContext.Request.GetStormpathAccount();
            var filterFactory = filterContext.RequestContext.HttpContext
                .GetOwinContext().Get<IAuthorizationFilterFactory>(OwinKeys.StormpathAuthorizationFilterFactory);

            var requireGroupsFilter = filterFactory.CreateGroupFilter(_allowedGroupNamesOrHrefs);
            var authorized = requireGroupsFilter.IsAuthorized(account);

            if (!authorized)
            {
                filterContext.Result = new HttpForbiddenResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }

        public new bool AllowMultiple => true;

        public override object TypeId => this;
    }
}
