namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Utils;
    using DJCWebApiBO.KB;
    using System;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/salecust")]
    public class SaleCustController : ApiController
    {
        [HttpGet, Route("salecustdetail")]
        public HttpResponseMessage processwastdatenum(int queryType, string custno, string buno, string yearmonth) => 
            HttpHelper.toJson(SaleCustBO.saleCustDetail(queryType, custno, buno, yearmonth));
    }
}

