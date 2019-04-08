namespace DJCWebApi.Controllers
{
    using DJCWebApi.Utils;
    using DJCWebApiBO.SQL;
    using PI.Core.DA;
    using System;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, RoutePrefix("api/sql")]
    public class SQLController : ApiController
    {
        [HttpGet, Route("exselect")]
        public HttpResponseMessage ExcuteSelect(string selectsql) => 
            HttpHelper.toJson(SQLExcutor.ExcuteSelect(selectsql));

        [HttpGet, Route("exproc")]
        public HttpResponseMessage ExcuteSP(string spname, SPParamers pars) => 
            HttpHelper.toJson(SQLExcutor.ExcuteSP(spname, pars));

        [HttpGet, Route("exprocnq")]
        public HttpResponseMessage ExcuteSPNoQuery(string spname, SPParamers pars) => 
            HttpHelper.toJson(SQLExcutor.ExcuteSPNoQuery(spname, pars));
    }
}

