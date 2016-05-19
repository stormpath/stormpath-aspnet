using System.Web.Http;

namespace Stormpath.AspNet.DocExamples
{
    #region code/authentication/aspnet/protected_route.cs
    [Authorize]
    public class SecretController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            // [Authorize] will require a logged-in user for this route
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