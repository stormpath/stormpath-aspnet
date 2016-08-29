using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Stormpath.SDK.Account;
using Stormpath.SDK.Sync;

namespace Stormpath.AspNet.Mvc
{
    /// <summary>
    /// The functionality of this attribute matches the [Authorize] attribute.  
    /// You can put multiple groups in a single call.  They will be treated like an "or".  
    /// If the user matches one of the groups listed, the call will be allowed.
    /// Use multiple [StormpathGroupsRequired] attributes to create "and" conditions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class StormpathGroupsRequiredAttribute : ActionFilterAttribute, IAuthenticationFilter
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

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!filterContext.Principal.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult("Not Authorized");
                return;
            }

            var account = filterContext.RequestContext.HttpContext.Request.GetStormpathAccount();
            if (account == null)
            {
                filterContext.Result = new HttpUnauthorizedResult("Not Authorized");
                return;
            }

            var groupNames = GetGroupNames(account);

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
                filterContext.Result = new HttpUnauthorizedResult("Not Authorized");
                return;
            }

            // At least one group matches; okay to continue!
        }

        public List<string> GetGroupNames(IAccount account)
        {
            return account.GetGroups().Synchronously().ToList().Select(g => g.Name).ToList();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }

        public new bool AllowMultiple => true;

        public override object TypeId => this;
    }
}
