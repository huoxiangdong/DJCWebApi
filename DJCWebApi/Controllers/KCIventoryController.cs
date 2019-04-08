namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Models;
    using DJCWebApi.Models.KCInventory;
    using DJCWebApi.Utils;
    using DJCWebApiBO.KCInventory;
    using PI.Core.DA;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/kcinventory")]
    public class KCIventoryController : ApiController
    {
        [HttpGet, Route("findCheckkcModelLike")]
        public HttpResponseMessage findCheckkcModelLike(string checkkcnopara)
        {
            CheckkcModel model = new CheckkcModel();
            List<CheckkcModel> list = new List<CheckkcModel>();
            List<DBData> list2 = CheckkcBO.findCheckkcModelLike(checkkcnopara);
            foreach (DBData data in list2)
            {
                CheckkcModel item = new CheckkcModel {
                    checkkcno = data["checkkcno"].ToString(),
                    ddate = data["ddate"].ToString(),
                    storageno = data["storageno"].ToString(),
                    storagename = data["storagename"].ToString() + "(" + data["storageno"].ToString() + ")",
                    checkmanname = data["checkmanname"].ToString(),
                    mkername = data["mkername"].ToString()
                };
                list.Add(item);
            }
            return model.toJson(list);
        }

        [HttpGet, Route("getCheckkcModel")]
        public HttpResponseMessage getCheckkcModel(string checkkcno)
        {
            CheckkcModel model = new CheckkcModel();
            List<DBData> list = CheckkcBO.getCheckkcModel(checkkcno);
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            if ((list != null) && (0 < list.Count))
            {
                str = list[0]["ddate"].ToString();
                str2 = list[0]["storageno"].ToString();
                str3 = list[0]["storagename"].ToString();
                str4 = list[0]["checkmanname"].ToString();
                str5 = list[0]["mkername"].ToString();
            }
            else if ((checkkcno == null) || ("" == checkkcno))
            {
                checkkcno = "";
            }
            else
            {
                str3 = "盘点单据有误";
            }
            model.checkkcno = checkkcno;
            model.ddate = str;
            model.storageno = str2;
            model.storagename = str3;
            model.checkmanname = str4;
            model.mkername = str5;
            return model.toJson(model);
        }

        [HttpGet, Route("getCheckkcModelList")]
        public HttpResponseMessage getCheckkcModelList()
        {
            CheckkcModel model = new CheckkcModel();
            List<CheckkcModel> list = new List<CheckkcModel>();
            List<DBData> list2 = CheckkcBO.getCheckkcModel();
            foreach (DBData data in list2)
            {
                CheckkcModel item = new CheckkcModel {
                    checkkcno = data["checkkcno"].ToString(),
                    ddate = data["ddate"].ToString(),
                    storageno = data["storageno"].ToString(),
                    storagename = data["storagename"].ToString() + "(" + data["storageno"].ToString() + ")",
                    checkmanname = data["checkmanname"].ToString(),
                    mkername = data["mkername"].ToString()
                };
                list.Add(item);
            }
            return model.toJson(list);
        }

        [HttpGet, Route("getPDDataCode")]
        public HttpResponseMessage getPDDataCode(string checkkcnopara, string datacodepara)
        {
            CheckkcDatacodeDetail detail = new CheckkcDatacodeDetail();
            JsonModel model = new JsonModel();
            List<CheckkcDatacodeDetail> list = new List<CheckkcDatacodeDetail>();
            List<DBData> list2 = CheckkcBO.getPDDataCode(checkkcnopara, datacodepara);
            string str = "";
            int num = 0;
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            decimal num2 = 0M;
            decimal num3 = 0M;
            decimal num4 = 0M;
            decimal num5 = 0M;
            string str7 = "";
            int num6 = 0;
            if ((list2 != null) && (0 < list2.Count))
            {
                str = list2[0]["checkkcno"].ToString();
                num = int.Parse(list2[0]["iid"].ToString());
                str3 = list2[0]["mcode"].ToString();
                str2 = list2[0]["datacode"].ToString();
                str4 = list2[0]["mname"].ToString();
                str5 = list2[0]["sprc"].ToString();
                str6 = list2[0]["units"].ToString();
                num2 = decimal.Parse(list2[0]["kcnum"].ToString());
                num3 = decimal.Parse(list2[0]["factnum"].ToString());
                num4 = decimal.Parse(list2[0]["kcnumelse"].ToString());
                num5 = decimal.Parse(list2[0]["factnumelse"].ToString());
                str7 = list2[0]["batchno"].ToString();
                num6 = int.Parse(list2[0]["pddiff"].ToString());
            }
            else if ((datacodepara != null) && ("" != datacodepara))
            {
                model.msg = "用户选择";
                model.success = true;
                model.userchoice = true;
                return model.toJson(model);
            }
            detail.checkkcno = str;
            detail.iid = num;
            detail.mcode = str3;
            detail.datacode = str2;
            detail.mname = str4;
            detail.sprc = str5;
            detail.units = str6;
            detail.kcnum = num2;
            detail.factnum = num3;
            detail.kcnumelse = num4;
            detail.factnumelse = num5;
            detail.batchno = str7;
            detail.pddiff = num6;
            return detail.toJson(detail);
        }

        [HttpGet, Route("getPDDataCodeElse")]
        public HttpResponseMessage getPDDataCodeElse(string checkkcnopara, string datacodepara)
        {
            CheckkcDatacodeDetail detail = new CheckkcDatacodeDetail();
            JsonModel model = new JsonModel();
            List<CheckkcDatacodeDetail> list = new List<CheckkcDatacodeDetail>();
            List<DBData> list2 = CheckkcBO.getPDDataCodeElse(checkkcnopara, datacodepara);
            string str = "";
            int num = 0;
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            decimal num2 = 0M;
            decimal num3 = 0M;
            string str7 = "";
            int num4 = 0;
            if ((list2 != null) && (0 < list2.Count))
            {
                str = list2[0]["checkkcno"].ToString();
                num = int.Parse(list2[0]["iid"].ToString());
                str3 = list2[0]["mcode"].ToString();
                str2 = list2[0]["datacode"].ToString();
                str4 = list2[0]["mname"].ToString();
                str5 = list2[0]["sprc"].ToString();
                str6 = list2[0]["units"].ToString();
                num2 = decimal.Parse(list2[0]["kcnum"].ToString());
                num3 = decimal.Parse(list2[0]["factnum"].ToString());
                str7 = list2[0]["batchno"].ToString();
                num4 = int.Parse(list2[0]["pddiff"].ToString());
            }
            detail.checkkcno = str;
            detail.iid = num;
            detail.mcode = str3;
            detail.datacode = str2;
            detail.mname = str4;
            detail.sprc = str5;
            detail.units = str6;
            detail.kcnum = num2;
            detail.factnum = num3;
            detail.batchno = str7;
            detail.pddiff = num4;
            return detail.toJson(detail);
        }

        [HttpGet, Route("getPDDatasDatacode")]
        public HttpResponseMessage getPDDatasDatacode(int querytype, string checkkcnopara, string datacodepara)
        {
            List<DBData> list = CheckkcBO.getkcmxdata(querytype, checkkcnopara, datacodepara);
            List<CheckkcDatacodeDetail> list2 = new List<CheckkcDatacodeDetail>();
            foreach (DBData data in list)
            {
                CheckkcDatacodeDetail item = new CheckkcDatacodeDetail {
                    checkkcno = data["checkkcno"].ToString(),
                    iid = int.Parse(data["iid"].ToString()),
                    mcode = data["mcode"].ToString(),
                    datacode = data["datacode"].ToString(),
                    mname = data["mname"].ToString(),
                    sprc = data["sprc"].ToString(),
                    units = data["units"].ToString(),
                    kcnum = decimal.Parse(data["kcnum"].ToString()),
                    factnum = decimal.Parse(data["factnum"].ToString()),
                    kcnumelse = decimal.Parse(data["kcnumelse"].ToString()),
                    factnumelse = decimal.Parse(data["factnumelse"].ToString()),
                    batchno = data["batchno"].ToString(),
                    pddiff = int.Parse(data["pddiff"].ToString()),
                    rkdt = data["rkdt"].ToString(),
                    barcode = data["barcode"].ToString()
                };
                list2.Add(item);
            }
            return HttpHelper.toJson(list2);
        }

        [HttpPost, Route("updateBoxPDDataCode")]
        public HttpResponseMessage updateBoxPDDataCode([FromBody] CheckkcFormVO para)
        {
            JsonModel model = new JsonModel();
            try
            {
                foreach (CheckkcView view in para.kcmxdatas)
                {
                    if (((view.factnum != view.kcnum) || (view.factnumelse != view.kcnumelse)) && (3 != view.pddiff))
                    {
                        view.pddiff = 2;
                    }
                    if (((view.factnum == view.kcnum) && (view.factnumelse == view.kcnumelse)) && (3 != view.pddiff))
                    {
                        view.pddiff = 1;
                    }
                    if (view.iid == 0)
                    {
                        CheckkcBO.insertPDDetailBox(view, para.storageno);
                    }
                    else
                    {
                        CheckkcBO.updateFactnum(view.checkkcno, view.iid, view.factnum, view.factnumelse, view.pddiff);
                    }
                }
            }
            catch (Exception exception)
            {
                model.msg = exception.Message;
                model.success = false;
                return model.toJson(model);
            }
            model.msg = "success";
            model.success = true;
            return model.toJson(model);
        }

        [HttpGet, Route("updatePDDataCode")]
        public HttpResponseMessage updatePDDataCode(string checkkcnopara, int iidpara, decimal factnumpara, decimal kcnumpara, decimal factnumelsepara, decimal kcnumelsepara, int pddiff, string storageno, string datacode)
        {
            JsonModel model = new JsonModel();
            if (((factnumpara != kcnumpara) || (factnumelsepara != kcnumelsepara)) && (3 != pddiff))
            {
                pddiff = 2;
            }
            if (((factnumpara == kcnumpara) && (factnumelsepara == kcnumelsepara)) && (3 != pddiff))
            {
                pddiff = 1;
            }
            try
            {
                if (iidpara == 0)
                {
                    CheckkcBO.insertPDDetail(checkkcnopara, iidpara, factnumpara, factnumelsepara, pddiff, storageno, datacode);
                }
                else
                {
                    CheckkcBO.updateFactnum(checkkcnopara, iidpara, factnumpara, factnumelsepara, pddiff);
                }
            }
            catch (Exception exception)
            {
                model.msg = exception.Message;
                model.success = false;
                return model.toJson(model);
            }
            model.msg = "success";
            model.success = true;
            return model.toJson(model);
        }
    }
}

