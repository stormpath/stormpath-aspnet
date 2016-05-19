using System.Web.Http;

namespace Stormpath.AspNet.DocExamples
{
    #region code/authentication/aspnet/protected_route.cs
    // Will require a logged-in user for the routes in this controller
    [Authorize]
    public class SecretController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            return "secret!";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            // do something
        }

    }
    #endregion
}