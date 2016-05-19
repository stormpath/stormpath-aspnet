using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Stormpath.SDK;

namespace Stormpath.AspNet.DocExamples
{
    public class Controller_RequestObjects : ApiController
    {
        #region code/request_objects/aspnet/extension_methods.cs
        public IHttpActionResult Get()
        {
            var client = Request.GetStormpathClient();
            var application = Request.GetStormpathApplication();
            var account = Request.GetStormpathAccount();

            // Do something with these objects...

            return Ok();
        }
        #endregion

        #region code/request_objects/aspnet/injecting_application.cs
        [HttpGet]
        public async Task<IHttpActionResult> FindAccountByEmail(string email)
        {
            var application = Request.GetStormpathApplication();

            var foundAccount = await application.GetAccounts()
                .Where(a => a.Email == email)
                .SingleOrDefaultAsync();

            if (foundAccount == null)
            {
                return Ok("No accounts found.");
            }
            else
            {
                return Ok(foundAccount.FullName);
            }
        }
        #endregion

        #region code/request_objects/aspnet/injecting_user.cs
        public async Task<IHttpActionResult> GetDetails()
        {
            var account = Request.GetStormpathAccount();

            // If the request is authenticated, do something with the account
            // (like get the account's Custom Data):
            if (account != null)
            {
                var customData = await account.GetCustomDataAsync();
                var favoriteColor = customData["favoriteColor"]?.ToString();

                return Ok(favoriteColor);
            }

            return Ok();
        }
        #endregion

        #region code/request_objects/aspnet/update_user_password.cs
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> UpdatePassword(string newPassword)
        {
            var account = Request.GetStormpathAccount();

            if (account != null)
            {
                account.SetPassword(newPassword);
                await account.SaveAsync();
            }

            return Ok();
        }
        #endregion
    }
}