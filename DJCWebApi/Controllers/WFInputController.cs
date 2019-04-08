namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Utils;
    using DJCWebApiBO.WFInOutPut;
    using PI.Core.DA;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/wfinput")]
    public class WFInputController : ApiController
    {
        [HttpPost, Route("scan")]
        public HttpResponseMessage BarcodeScan([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            List<OptStep> list = wfioBO.getBarcodeActions(para.machno, para.datacode, para.morder);
            if (list.Count == 1)
            {
                switch (list[0].Step)
                {
                    case 0:
                        return this.MaterialUp(para);

                    case 3:
                        return this.ProductUp(para);

                    case 6:
                        return HttpHelper.toJson(list);
                }
            }
            return HttpHelper.toJson(list);
        }

        [HttpPost, Route("createheadgroup")]
        public HttpResponseMessage createHeadGroup([FromBody] DBData value)
        {
            int? change = null;
            if (value.getValue("change") > null)
            {
                change = new int?(int.Parse(value.getValue("change").ToString()));
            }
            return HttpHelper.toJson(wfioBO.createHeadGroup((string) value.getValue("machine"), change, (string) value.getValue("heads")));
        }

        [HttpGet, Route("machinebysn")]
        public HttpResponseMessage getMachineBySn(string barcode) => 
            HttpHelper.toJson(wfioBO.getMachineBySn(base.User.Identity.Name, barcode));

        [HttpGet, Route("machinedetail")]
        public HttpResponseMessage getMachineDetail(string machine) => 
            HttpHelper.toJson(wfioBO.getMachineDetail(base.User.Identity.Name, machine));

        [HttpGet, Route("heads")]
        public HttpResponseMessage getMachineHeads(string machine) => 
            HttpHelper.toJson(wfioBO.getMachineHeads(machine));

        [HttpGet, Route("machine")]
        public HttpResponseMessage getMachineState() => 
            HttpHelper.toJson(wfioBO.getMachineState(base.User.Identity.Name));

        [HttpGet, Route("getqkout")]
        public HttpResponseMessage getqkout(string machno) => 
            HttpHelper.toJson(wfioBO.checkMachineQKOut(machno));

        [HttpPost, Route("MaterialDown")]
        public HttpResponseMessage MaterialDown([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.MaterialDown(para.empno, para.datacode);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("MaterialDownEmpty")]
        public HttpResponseMessage MaterialDownEmpty([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.MaterialDown(para.empno, para.datacode, true);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("MaterialUp")]
        public HttpResponseMessage MaterialUp([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.MaterialUp(para.empno, para.machno, para.datacode);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("ProductDown")]
        public HttpResponseMessage ProductDown([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.ProductDown(para.empno, para.datacode, para.length);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("ProductDownEmpty")]
        public HttpResponseMessage ProductDownEmpty([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.ProductDown(para.empno, para.datacode, para.length, true);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("ProductUp")]
        public HttpResponseMessage ProductUp([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.ProductUp(para.empno, para.machno, para.datacode);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("SwitchMachine")]
        public HttpResponseMessage SwitchMachine([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.SwitchMachine(para.empno, para.machno, para.datacode);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.datacode));
        }

        [HttpPost, Route("SwitchMaterial")]
        public HttpResponseMessage SwitchMaterial([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.SwitchMaterial(para.empno, para.machno, para.matsn, para.datacode);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("SwitchMaterialEmpty")]
        public HttpResponseMessage SwitchMaterialEmpty([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.SwitchMaterial(para.empno, para.machno, para.matsn, para.datacode, true);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("SwitchProduct")]
        public HttpResponseMessage SwitchProduct([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.SwitchProduct(para.empno, para.machno, para.prosn, para.length, para.datacode);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }

        [HttpPost, Route("SwitchProductEmpty")]
        public HttpResponseMessage SwitchProductEmpty([FromBody] BatchcodeVO para)
        {
            para.empno = base.User.Identity.Name;
            wfioBO.SwitchProduct(para.empno, para.machno, para.prosn, para.length, para.datacode, true);
            return HttpHelper.toJson(wfioBO.getMachineDetail(para.empno, para.machno));
        }
    }
}

