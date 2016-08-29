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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class StormpathCustomDataRequiredAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        private readonly string key;
        private readonly object value;

        public StormpathCustomDataRequiredAttribute(string key, object value)
        {
            this.key = key;
            this.value = value;
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

            var customData = account.GetCustomData();

            if (customData?[key] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult("Not Authorized");
                return;
            }

            if (!customData[key].Equals(value))
            {
                filterContext.Result = new HttpUnauthorizedResult("Not Authorized");
                return;
            }

            // The Custom Data matches; okay to continue!
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }

        public new bool AllowMultiple => true;

        public override object TypeId => this;
    }
}
