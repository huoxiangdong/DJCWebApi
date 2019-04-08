namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Models.workfloor;
    using DJCWebApi.Utils;
    using DJCWebApiBO.FQC;
    using DJCWebApiBO.User;
    using PI.Core.DA;
    using PI.Core.vo;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/fqc")]
    public class FQCController : ApiController
    {
        [HttpGet, Route("Count")]
        public HttpResponseMessage getCount(string datacode)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<QCCountVO> list = FqcBO.getCount(datacode);
            if ((list == null) || (list.Count <= 0))
            {
                return HttpHelper.toJson(new QCCountVO());
            }
            return HttpHelper.toJson(list[0]);
        }

        [HttpGet, Route("CountDetail")]
        public HttpResponseMessage getCountDetail(string datacode)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            return HttpHelper.toJson(FqcBO.getCountDetail(datacode));
        }

        [HttpGet, Route("DataCode")]
        public HttpResponseMessage getDataCode(string datacode)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<BaCpinfoVO> list = FqcBO.getCpinfoByDatacode2(datacode, userByPk.Code);
            if ((list == null) || (list.Count <= 0))
            {
                throw new Exception("该标记号无对应记录！");
            }
            return HttpHelper.toJson(list[0]);
        }

        [HttpGet, Route("DataCode")]
        public HttpResponseMessage getDataCode(string datacode, bool qualified, string itemno, string qcno)
        {
            BaCpinfo cpinfo = new BaCpinfo();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            if ((qcno == null) || ("" == qcno))
            {
                FqcBO.createNewFqcVO(datacode, qualified, itemno, userByPk.Code);
            }
            else
            {
                FqcBO.updateFqcVO(datacode, qualified, itemno, userByPk.Code, qcno);
            }
            return cpinfo.toJson(cpinfo);
        }

        [HttpGet, Route("DataCodeforAdjust")]
        public HttpResponseMessage getDataCodeforAdjust(string datacode)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list = FqcBO.getDataCodeforAdjust(datacode);
            if ((list == null) || (list.Count <= 0))
            {
                throw new Exception("该标记号无对应FQC记录！");
            }
            return HttpHelper.toJson(list[0]);
        }

        [HttpGet, Route("getMordernos")]
        public HttpResponseMessage getMordernos(string morderno)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            return HttpHelper.toJson(FqcBO.getMordernos(morderno));
        }

        [HttpGet, Route("getProstd")]
        public HttpResponseMessage getProstd()
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            return HttpHelper.toJson(FqcBO.getProstd());
        }

        [HttpGet, Route("getQCBadList")]
        public HttpResponseMessage getQCBadList()
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<QCBadItemModel> list = new List<QCBadItemModel>();
            QCBadItemModel model = new QCBadItemModel();
            List<DBData> list2 = FqcBO.getCheItem();
            foreach (DBData data in list2)
            {
                QCBadItemModel item = new QCBadItemModel {
                    itemno = data["itemno"].ToString(),
                    itemname = data["itemname"].ToString()
                };
                list.Add(item);
            }
            return model.toJson(list);
        }

        [HttpGet, Route("getQCBadListByProstd")]
        public HttpResponseMessage getQCBadListByProstd(int prostdno)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            return HttpHelper.toJson(FqcBO.getCheItemByProstd(prostdno));
        }

        [HttpGet, Route("getQCCJList")]
        public HttpResponseMessage getQCCJList()
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<QCBadItemModel> list = new List<QCBadItemModel>();
            QCBadItemModel model = new QCBadItemModel();
            List<DBData> list2 = FqcBO.getRepairItem();
            foreach (DBData data in list2)
            {
                QCBadItemModel item = new QCBadItemModel {
                    itemno = data["itemno"].ToString(),
                    itemname = data["itemname"].ToString()
                };
                list.Add(item);
            }
            return model.toJson(list);
        }

        [HttpGet, Route("getQCRepairListByProstd")]
        public HttpResponseMessage getQCRepairListByProstd(int prostdno)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            return HttpHelper.toJson(FqcBO.getCheRPItemByProstd(prostdno));
        }

        [HttpGet, Route("UpdateAdjust")]
        public HttpResponseMessage updateAdjust(string qcno, int adjudication, string repairtype)
        {
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            FqcBO.updateAdjust(qcno, adjudication, repairtype);
            return HttpHelper.toJson(1);
        }
    }
}

