using System.Web.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Route("/config")]
    public class ConfigController : Controller
    {
        public string Index()
        {
            return Request.GetStormpathConfiguration().Application.Name;
        }
    }
}