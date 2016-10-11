using System.Web.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Route("/application")]
    public class ApplicationController : Controller
    {
        public string Index()
        {
            return Request.GetStormpathApplication().Href;
        }
    }
}