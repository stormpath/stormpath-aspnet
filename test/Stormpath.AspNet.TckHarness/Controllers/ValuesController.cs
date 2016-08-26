using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Stormpath.AspNet;
using Stormpath.AspNet.Web.Http.Filters;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        //[StormpathGroupsRequired("Test")]
        //[StormpathGroupsRequired("Hello")]
        //[StormpathCustomDataRequired("isAdmin", true)]
        public string Get(CancellationToken ct)
        {
            var account = Request.GetStormpathAccount();

            return account?.FullName;
        }
    }
}
