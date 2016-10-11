using System.Threading.Tasks;
using System.Web.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Route("/client")]
    public class ClientController : Controller
    {
        public async Task<string> Index()
        {
            var tenant = await Request.GetStormpathClient().GetCurrentTenantAsync();

            return tenant.Href;
        }
    }
}