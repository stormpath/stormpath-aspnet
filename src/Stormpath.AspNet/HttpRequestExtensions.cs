using System.Net.Http;
using System.Web;
using Stormpath.Configuration.Abstractions.Immutable;
using Stormpath.Owin.Abstractions;
using Stormpath.SDK.Account;
using Stormpath.SDK.Application;
using Stormpath.SDK.Client;
using Stormpath.SDK.Sync;

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

        public static StormpathConfiguration GetStormpathConfiguration(this HttpRequestBase request)
            => request.GetOwinContext().Get<StormpathConfiguration>(OwinKeys.StormpathConfiguration);

        public static StormpathConfiguration GetStormpathConfiguration(this HttpRequestMessage request)
            => request.GetOwinContext().Get<StormpathConfiguration>(OwinKeys.StormpathConfiguration);

        public static IApplication GetStormpathApplication(this HttpRequestBase request)
        {
            var client = request.GetStormpathClient();
            var configuration = request.GetStormpathConfiguration();

            return client.GetApplication(configuration.Application.Href);
        }

        public static IApplication GetStormpathApplication(this HttpRequestMessage request)
        {
            var client = request.GetStormpathClient();
            var configuration = request.GetStormpathConfiguration();

            return client.GetApplication(configuration.Application.Href);
        }
    }
}
