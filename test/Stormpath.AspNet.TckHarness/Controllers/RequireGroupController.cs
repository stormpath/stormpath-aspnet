using System.Web.Mvc;
using Stormpath.AspNet.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Route("/requireGroup")]
    [Authorize]
    [StormpathGroupsRequired("adminIT")]
    public class RequireGroupController : Controller
    {
        public string Index()
        {
            return "Ok";
        }
    }
}