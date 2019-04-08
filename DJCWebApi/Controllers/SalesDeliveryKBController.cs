namespace DJCWebApi.Controllers
{
    using DJCWebApi.Utils;
    using DJCWebApiBO.KB;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, RoutePrefix("api/kb/salesdelivery")]
    public class SalesDeliveryKBController : ApiController
    {
        [HttpGet, Route("summry")]
        public HttpResponseMessage Machine() => 
            HttpHelper.toJson(SalesDeliveryKB.querySummary());
    }
}

