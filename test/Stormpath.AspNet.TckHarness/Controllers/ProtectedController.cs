using System.Web.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Route("/protected")]
    [Authorize]
    public class ProtectedController : Controller
    {
        public string Index()
        {
            return "Ok";
        }
    }
}