using System.Web.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            // The Stormpath Client can be retrieved from the request
            var client = Request.GetStormpathClient();

            // The current logged in Stormpath Account (if any) can be retrieved as well
            var account = Request.GetStormpathAccount();

            return View();
        }
    }
}