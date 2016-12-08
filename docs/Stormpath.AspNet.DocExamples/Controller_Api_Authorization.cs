using System.Web.Http;

namespace Stormpath.AspNet.DocExamples
{
    #region code/authorization/aspnet/protected_route.cs
    // Will require a logged-in user for the routes in this controller
    [Authorize]
    public class SecretController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "secret!";
        }
    }
    #endregion
}