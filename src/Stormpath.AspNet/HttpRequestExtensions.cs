using System.Net.Http;
using System.Web;
using Stormpath.Owin.Abstractions;
using Stormpath.SDK.Account;
using Stormpath.SDK.Client;

namespace Stormpath.AspNet
{
    public static class HttpRequestExtensions
    {
        public static IClient GetStormpathClient(this HttpRequestBase request)
            => request.GetOwinContext().Get<IClient>(OwinKeys.StormpathClient);

        public static IClient GetStormpathClient(this HttpRequestMessage request)
            => request.GetOwinContext().Get<IClient>(OwinKeys.StormpathClient);

        public static IAccount GetStormpathAccount(this HttpRequestBase request)
            => request.GetOwinContext().Get<IAccount>(OwinKeys.StormpathUser);

        public static IAccount GetStormpathAccount(this HttpRequestMessage request)
            => request.GetOwinContext().Get<IAccount>(OwinKeys.StormpathUser);
    }
}
