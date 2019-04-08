namespace DJCWebApi.Controllers
{
    using DJCWebApi.Results;
    using PI.Core;
    using PI.Core.BL;
    using PI.Core.vo;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    public class WSController : ApiController
    {
        public HttpResponseMessage Get()
        {
            try
            {
                string clientIpAddress = base.Request.GetClientIpAddress();
                IPPolicy policy = IPAddressManager.Manager.CheckPolicy(clientIpAddress);
                IPRegion region = IPAddressManager.Manager.CheckRegion(clientIpAddress);
                if ((((region != IPRegion.Inside) && (region != IPRegion.All)) || (policy == IPPolicy.Forbiden)) || !clientIpAddress.StartsWith(":"))
                {
                    return new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
            if (HttpContext.Current.IsWebSocketRequest)
            {
                try
                {
                    HttpContext.Current.AcceptWebSocketRequest(new Func<AspNetWebSocketContext, Task>(WebSocketContext.ProcessWSChat));
                }
                catch (Exception exception2)
                {
                    Logger.Error(exception2);
                }
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
    }
}

