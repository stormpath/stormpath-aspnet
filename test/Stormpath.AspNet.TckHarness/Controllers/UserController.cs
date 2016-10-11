using System.Net;
using System.Web.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var accountHref = Request.GetStormpathAccount()?.Href;

            if (string.IsNullOrEmpty(accountHref))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NoContent);
            }

            return new ContentResult() {Content = accountHref};
        }
    }
}