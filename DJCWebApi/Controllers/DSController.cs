namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Utils;
    using PI.Core.DA;
    using pi.ds;
    using System;
    using System.Collections.Specialized;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/ds")]
    public class DSController : ApiController
    {
        [HttpGet, Route("data/{group}/{name}")]
        public HttpResponseMessage Get(string group, string name)
        {
            DBData paras = this.getParams(base.Request);
            string identity = group + name;
            if (DataServiceManager.Manager.isContain(identity))
            {
                DBData data2 = DataServiceManager.Manager.getTaskData(identity, paras);
                if (data2.isCollection)
                {
                    return HttpHelper.toJson(data2.Items);
                }
                return HttpHelper.toJson(data2);
            }
            return HttpHelper.toJson(new DBData());
        }

        private DBData getParams(HttpRequestMessage request)
        {
            DBData data = new DBData();
            NameValueCollection values = base.Request.RequestUri.ParseQueryString();
            foreach (string str in values.AllKeys)
            {
                data.Add(str, values[str]);
            }
            return data;
        }
    }
}

