using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Stormpath.AspNet
{
    public class StormpathAuthenticationMiddleware : AuthenticationMiddleware<StormpathAuthenticationOptions>
    {
        public StormpathAuthenticationMiddleware(OwinMiddleware next, StormpathAuthenticationOptions options)
            : base(next, options)
        {
            //if (string.IsNullOrEmpty(Options.SignInAsAuthenticationType))
            //{
            //    options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
            //}
            //if (options.StateDataFormat == null)
            //{
            //    var dataProtector = app.CreateDataProtector(typeof(DummyAuthenticationMiddleware).FullName,
            //        options.AuthenticationType);

            //    options.StateDataFormat = new PropertiesDataFormat(dataProtector);
            //}
        }

        // Called for each request, to create a handler for each request.
        protected override AuthenticationHandler<StormpathAuthenticationOptions> CreateHandler()
        {
            return new StormpathAuthenticationHandler();
        }
    }
}
