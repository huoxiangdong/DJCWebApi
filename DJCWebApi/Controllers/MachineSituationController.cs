namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Utils;
    using DJCWebApiBO.Machine;
    using System;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/machinesituation")]
    public class MachineSituationController : ApiController
    {
        [HttpGet, Route("machinejdl")]
        public HttpResponseMessage machinejdl(string dateBegin, string dateEnd, string machine) => 
            HttpHelper.toJson(MachineBO.getMachineJDLVO(dateBegin, dateEnd, machine));

        [HttpGet, Route("machinejdlab")]
        public HttpResponseMessage machinejdlab(string dateBegin, string dateEnd, string machine) => 
            HttpHelper.toJson(MachineBO.getMachineJDLABVO(dateBegin, dateEnd, machine));

        [HttpGet, Route("machinemodel")]
        public HttpResponseMessage machinemodelcount(string prostdno) => 
            HttpHelper.toJson(MachineBO.getMachineModelCountVO(prostdno));

        [HttpGet, Route("machineoee")]
        public HttpResponseMessage machineoee(string machine) => 
            HttpHelper.toJson(MachineBO.getMachineOEEVO(machine));

        [HttpGet, Route("processcount")]
        public HttpResponseMessage processcount() => 
            HttpHelper.toJson(MachineBO.getMachineProcessCountVO());

        [HttpGet, Route("processoeesummary")]
        public HttpResponseMessage processoee(string dateBegin, string dateEnd, int prosesstype, int type) => 
            HttpHelper.toJson(MachineBO.querySummary(dateBegin, dateEnd, prosesstype, type));

        [HttpGet, Route("processoeesum")]
        public HttpResponseMessage processoeesum(string dateBegin, string dateEnd, int prosesstype, int type) => 
            HttpHelper.toJson(MachineBO.getProcessOEESumVO(dateBegin, dateEnd, prosesstype, type));
    }
}

