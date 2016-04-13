using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stormpath.AspNet.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        // Stormpath Client can be injected using DI
        //[FromServices]
        //public IClient StormpathClient { get; set; }

        //[FromServices]
        //public IAccount StormpathUser { get; set; }

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }
    }
}