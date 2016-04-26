using System.Web;
using Stormpath.Owin.Abstractions;
using Stormpath.SDK.Account;
using Stormpath.SDK.Client;

namespace Stormpath.AspNet
{
    public static class HttpRequestBaseExtensions
    {
        public static IClient GetStormpathClient(this HttpRequestBase request)
            => request.GetOwinContext().Get<IClient>(OwinKeys.StormpathClient);

        public static IAccount GetStormpathAccount(this HttpRequestBase request)
            => request.GetOwinContext().Get<IAccount>(OwinKeys.StormpathUser);
    }
}
