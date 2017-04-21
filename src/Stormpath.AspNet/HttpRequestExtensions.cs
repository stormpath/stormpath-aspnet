using System.Net.Http;
using System.Web;
using Stormpath.Configuration.Abstractions.Immutable;
using Stormpath.Owin.Abstractions;

namespace Stormpath.AspNet
{
    public static class HttpRequestExtensions
    {
        public static ICompatibleOktaAccount GetStormpathAccount(this HttpRequestBase request)
            => request.GetOwinContext().Get<ICompatibleOktaAccount>(OwinKeys.StormpathUser);

        public static ICompatibleOktaAccount GetStormpathAccount(this HttpRequestMessage request)
            => request.GetOwinContext().Get<ICompatibleOktaAccount>(OwinKeys.StormpathUser);

        public static StormpathConfiguration GetStormpathConfiguration(this HttpRequestBase request)
            => request.GetOwinContext().Get<StormpathConfiguration>(OwinKeys.StormpathConfiguration);

        public static StormpathConfiguration GetStormpathConfiguration(this HttpRequestMessage request)
            => request.GetOwinContext().Get<StormpathConfiguration>(OwinKeys.StormpathConfiguration);
    }
}
