using System.Threading;
using System.Web.Http;
using Stormpath.AspNet.WebApi;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        public string Get(CancellationToken ct)
        {
            var account = Request.GetStormpathAccount();

            return account?.FullName;
        }
    }
}
