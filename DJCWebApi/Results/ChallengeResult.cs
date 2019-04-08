namespace DJCWebApi.Results
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class ChallengeResult : IHttpActionResult
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <LoginProvider>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HttpRequestMessage <Request>k__BackingField;

        public ChallengeResult(string loginProvider, ApiController controller)
        {
            this.LoginProvider = loginProvider;
            this.Request = controller.Request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            string[] authenticationTypes = new string[] { this.LoginProvider };
            this.Request.GetOwinContext().Authentication.Challenge(authenticationTypes);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.Unauthorized) {
                RequestMessage = this.Request
            };
            return Task.FromResult<HttpResponseMessage>(result);
        }

        public string LoginProvider { get; set; }

        public HttpRequestMessage Request { get; set; }
    }
}

