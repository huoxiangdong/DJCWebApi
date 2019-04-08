namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Results;
    using DJCWebApi.Utils;
    using PI.Core.BL;
    using PI.Core.DA;
    using PI.Core.vo;
    using System;
    using System.Net.Http;
    using System.Web.Http;

    [WebApiExceptionFilter, RoutePrefix("api/ipv")]
    public class IPValidateController : ApiController
    {
        [HttpGet, Route("ip")]
        public HttpResponseMessage Get()
        {
            string clientIpAddress = base.Request.GetClientIpAddress();
            IPPolicy policy = IPAddressManager.Manager.CheckPolicy(clientIpAddress);
            IPRegion region = IPAddressManager.Manager.CheckRegion(clientIpAddress);
            DBData data = new DBData();
            data.Add("policy", policy);
            data.Add("region", region);
            data.Add("clientip", clientIpAddress);
            return HttpHelper.toJson(data);
        }
    }
}

