using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stormpath.AspNet.Mvc;

namespace Stormpath.AspNet.TckHarness.Controllers
{
    [Authorize]
    [StormpathGroupsRequired("Test")]
    //[StormpathGroupsRequired("Hello")]
    //[StormpathCustomDataRequired("isAdmin", true)]
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
    }
}