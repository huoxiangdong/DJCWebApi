namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Utils;
    using DJCWebApiBO.StockAge;
    using System;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/stockage")]
    public class StockAgeController : ApiController
    {
        [HttpGet, Route("processwastdatenum")]
        public HttpResponseMessage processwastdatenum(string prostdno, string queryDate) => 
            HttpHelper.toJson(StockAgeBO.queryProcessWastDatenum(prostdno, queryDate));

        [HttpGet, Route("processwastmcodenum")]
        public HttpResponseMessage processwastmcodenum(string prostdno) => 
            HttpHelper.toJson(StockAgeBO.queryProcessWastMcodenum(prostdno, null));

        [HttpGet, Route("processwastmcodenum")]
        public HttpResponseMessage processwastmcodenum(string prostdno, string mcode) => 
            HttpHelper.toJson(StockAgeBO.queryProcessWastMcodenum(prostdno, mcode));

        [HttpGet, Route("stockagedetail")]
        public HttpResponseMessage stockagedetail(string storagecode) => 
            HttpHelper.toJson(StockAgeBO.queryStockAgeData(storagecode));

        [HttpGet, Route("stockagemcodedetail")]
        public HttpResponseMessage stockagemcodedetail(string storagecode, int stockageno, string cust) => 
            HttpHelper.toJson(StockAgeBO.queryStockAgeMcodeData(storagecode, stockageno, cust));
    }
}

