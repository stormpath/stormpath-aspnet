using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Stormpath.AspNet.WebApi
{
    public class AuthorizationFailureResult : IHttpActionResult
    {
        public AuthorizationFailureResult(HttpStatusCode statusCode, string reasonPhrase, HttpRequestMessage request)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public HttpStatusCode StatusCode { get; }

        public string ReasonPhrase { get; }

        public HttpRequestMessage Request { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(StatusCode)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };

            return Task.FromResult(response);
        }
    }
}
