using System.Web.Mvc;
using Stormpath.AspNet.Mvc;

namespace Stormpath.AspNet.DocExamples
{
    #region code/authorization/aspnet/require_single_group.cs
    [Authorize]
    [StormpathGroupsRequired("admin")]
    public class AdminController : Controller
    {
        // Only users in the admin group can access these actions

        public ActionResult Index()
        {
            return View();
        }
    }
    #endregion

    public class Snippets2 : Controller
    {
        #region code/authorization/aspnet/require_group_by_href.cs
        [StormpathGroupsRequired("https://api.stormpath.com/v1/groups/aRaNdOmGrOuPiDhEre")]
        public ActionResult ManagePayment()
        {
            return View();
        }
        #endregion      
    }

    public class Snippets3 : Controller
    {
        #region code/authorization/aspnet/require_any_group.cs
        [StormpathGroupsRequired("subscriber", "partner")]
        public ActionResult ManagePayment()
        {
            return View();
        }
        #endregion
    }

    #region code/authorization/aspnet/require_multiple_groups.cs
    [Authorize]
    [StormpathGroupsRequired("admin")]
    [StormpathGroupsRequired("manager")]
    public class AdminManagementController : Controller
    {
        // Only users in BOTH the admin and manager
        // groups can access these actions

        public ActionResult Index()
        {
            return View();
        }
    }
    #endregion

    #region code/authorization/aspnet/require_customData.cs
    [Authorize]
    [StormpathCustomDataRequired("canPost", true)]
    public class CreatePostController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
    #endregion

    #region code/authorization/aspnet/require_multiple_customData.cs
    [Authorize]
    [StormpathCustomDataRequired("canPost", true)]
    [StormpathCustomDataRequired("userType", "admin")]
    public class CreateStickyPostController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
    #endregion
}
