using System.Web.Mvc;

namespace Stormpath.AspNet.Mvc
{
    public class HttpForbiddenResult : HttpStatusCodeResult
    {
        public HttpForbiddenResult()
            : base(403, "Not authorized")
        {
        }
    }
}
