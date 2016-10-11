using System.Web.Mvc;
using Stormpath.AspNet.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Route("/requireCustomData")]
    [Authorize]
    [StormpathCustomDataRequired("testing", "rocks!")]
    public class RequireCustomDataController : Controller
    {
        public string Index()
        {
            return "Ok";
        }
    }
}