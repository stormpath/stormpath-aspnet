using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Stormpath.Owin.Abstractions;
using Stormpath.Owin.Middleware;

namespace Stormpath.AspNet.Mvc
{
    /// <summary>
    /// Requires a matching Stormpath Custom Data element to be present for the user.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class StormpathCustomDataRequiredAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        private readonly string _key;
        private readonly object _value;
        private readonly IEqualityComparer<object> _comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="StormpathCustomDataRequiredAttribute"/> class.
        /// </summary>
        /// <param name="key">The Custom Data key.</param>
        /// <param name="value">The Custom Data value.</param>
        public StormpathCustomDataRequiredAttribute(string key, object value)
            : this(key, value, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StormpathCustomDataRequiredAttribute"/> class.
        /// </summary>
        /// <param name="key">The Custom Data key.</param>
        /// <param name="value">The Custom Data value.</param>
        /// <param name="comparer">The comparer to use when comparing Custom Data values.</param>
        public StormpathCustomDataRequiredAttribute(string key, object value, IEqualityComparer<object> comparer)
        {
            _key = key;
            _value = value;
            _comparer = comparer;
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

            var requireCustomDataFilter = filterFactory.CreateCustomDataFilter(_key, _value, _comparer);
            var authorized = requireCustomDataFilter.IsAuthorized(account);

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
