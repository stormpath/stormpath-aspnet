using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Stormpath.AspNet.WebApi
{
    public class AuthorizationFailureResult : IHttpActionResult
    {
        public AuthorizationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;

            return Task.FromResult(response);
        }
    }
}
