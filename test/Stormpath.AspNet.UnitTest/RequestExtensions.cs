using System.Collections.Generic;
using System.Net.Http;
using Stormpath.Owin.Abstractions;
using Stormpath.SDK.Account;

namespace Stormpath.AspNet.UnitTest
{
    public static class RequestExtensions
    {
        public static void SetStormpathAccount(this HttpRequestMessage request, IAccount account)
        {
            if (request.GetOwinContext() == null)
            {
                request.SetOwinEnvironment(new Dictionary<string, object>());
            }

            request.GetOwinContext().Set(OwinKeys.StormpathUser, account);
            request.GetOwinContext().Set(OwinKeys.StormpathClient, account.Client);
        }
    }
}
