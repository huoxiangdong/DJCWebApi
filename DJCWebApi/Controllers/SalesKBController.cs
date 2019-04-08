namespace DJCWebApi.Controllers
{
    using DJCWebApi.Utils;
    using DJCWebApiBO.KB;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, RoutePrefix("api/kb/sales")]
    public class SalesKBController : ApiController
    {
        [HttpGet, Route("summry")]
        public HttpResponseMessage Machine() => 
            HttpHelper.toJson(SalesKB.querySummary());
    }
}

