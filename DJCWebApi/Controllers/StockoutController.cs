namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Models.stockout;
    using DJCWebApi.Utils;
    using DJCWebApiBO.KB;
    using DJCWebApiBO.StockOut;
    using DJCWebApiBO.User;
    using PI.Core.DA;
    using PI.Core.vo;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/stockout")]
    public class StockoutController : ApiController
    {
        public HttpResponseMessage Get() => 
            new HttpResponseMessage { Content = new StringContent("value", Encoding.UTF8, "application/json") };

        [HttpGet, Route("getckNoticeList")]
        public HttpResponseMessage getckNoticeList(string sourceid, int outtype)
        {
            string outno = "";
            int back = 0;
            string msgback = "";
            bool flag = false;
            List<StockoutModel> list = new List<StockoutModel>();
            StockoutModel model = new StockoutModel();
            Dictionary<string, StockoutModel> dictionary = new Dictionary<string, StockoutModel>();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockOutBO.cknoticeQuery(sourceid, outtype, userByPk.Code, ref outno, ref back, ref msgback);
            List<DBData> list3 = new List<DBData>();
            List<DBData> list4 = new List<DBData>();
            if ((((sourceid != null) && ("" != sourceid)) && (outno != null)) && ("" != outno))
            {
                list3 = StockOutBO.cknoticeGetOutList(sourceid, outtype, outno);
            }
            if ((sourceid != null) && ("" != sourceid))
            {
                list4 = StockOutBO.cknoticeGetStoreList(sourceid, outtype, userByPk.Code, outno);
            }
            foreach (DBData data in list2)
            {
                StockoutModel model2 = new StockoutModel();
                StockoutHeadModel item = new StockoutHeadModel();
                StockoutDetailModel model4 = new StockoutDetailModel();
                if (!dictionary.ContainsKey(data["custno"].ToString()))
                {
                    model2.custno = data["custno"].ToString();
                    model2.shortname = data["shortname"].ToString();
                    dictionary.Add(data["custno"].ToString(), model2);
                    dictionary[data["custno"].ToString()].Stockout = new List<StockoutHeadModel>();
                }
                dictionary.TryGetValue(data["custno"].ToString(), out model2);
                foreach (StockoutHeadModel model5 in model2.Stockout)
                {
                    if (model5.sourceid == data["outno"].ToString())
                    {
                        flag = true;
                        item = model5;
                        break;
                    }
                }
                if (!flag)
                {
                    item.outno = data["curoutno"].ToString();
                    item.sourceid = data["outno"].ToString();
                    item.ddate = Convert.ToString(data["ddate"]);
                    item.custno = data["custno"].ToString();
                    item.shortname = data["shortname"].ToString();
                    item.Detail = new List<StockoutDetailModel>();
                    model2.Stockout.Add(item);
                }
                flag = false;
                model4.outno = data["curoutno"].ToString();
                model4.sourceid = data["outno"].ToString();
                model4.autoid = Convert.ToInt16(data["autoid"]);
                model4.mcode = data["mcode"].ToString();
                model4.coptypeno = data["coptypeno"].ToString();
                model4.sprc = data["sprc"].ToString();
                model4.mname = data["mname"].ToString();
                model4.mnum = Convert.ToDecimal(data["mnum"]);
                model4.kcnum = Convert.ToDecimal(0);
                model4.kcdvlnum = Convert.ToDecimal(0);
                model4.curunfqty = Convert.ToDecimal(data["curunfqty"]);
                model4.allselqty = Convert.ToDecimal(data["allselqty"]);
                model4.curselqty = Convert.ToDecimal(data["curselqty"]);
                model4.Batchcode = new List<StockoutListModel>();
                model4.Storelist = new List<StockoutStoreListModel>();
                foreach (DBData data2 in list4)
                {
                    StockoutStoreListModel model6 = new StockoutStoreListModel();
                    if (((model4.sourceid == data2["sourceid"].ToString()) && (model4.mcode == data2["mcode"].ToString())) && (model4.coptypeno == data2["coptypeno"].ToString()))
                    {
                        model6.batchno = data2["batchno"].ToString();
                        model6.datacode = data2["datacode"].ToString();
                        model6.listid = Convert.ToInt16(data2["listid"]);
                        model6.mcode = data2["mcode"].ToString();
                        model6.coptypeno = data2["coptypeno"].ToString();
                        model6.mnum = Convert.ToDecimal(data2["mnum"]);
                        model6.kcdvlnum = Convert.ToDecimal(data2["kcdvlnum"]);
                        model6.outno = data2["curoutno"].ToString();
                        model6.rkdt = data2["rkdt"].ToString();
                        model6.sourceid = data2["sourceid"].ToString();
                        model6.sprc = data2["sprc"].ToString();
                        model6.storageno = data2["storageno"].ToString();
                        model6.storeno = data2["storeno"].ToString();
                        model4.kcnum = decimal.Add(model4.kcnum, model6.mnum);
                        model4.kcdvlnum = decimal.Add(model4.kcdvlnum, model6.kcdvlnum);
                        model4.Storelist.Add(model6);
                    }
                }
                foreach (DBData data3 in list3)
                {
                    StockoutListModel model7 = new StockoutListModel();
                    if (((model4.sourceid == data3["sourceid"].ToString()) && (model4.mcode == data3["mcode"].ToString())) && (model4.coptypeno == data3["coptypeno"].ToString()))
                    {
                        model7.outno = data3["outno"].ToString();
                        model7.sourceid = data3["sourceid"].ToString();
                        model7.autoid = Convert.ToInt16(data3["autoid"]);
                        model7.iid = Convert.ToInt16(data3["iid"]);
                        model7.mcode = data3["mcode"].ToString();
                        model7.coptypeno = data3["coptypeno"].ToString();
                        model7.sprc = data3["sprc"].ToString();
                        model7.mnum = Convert.ToDecimal(data3["mnum"]);
                        model7.datacode = data3["datacode"].ToString();
                        model7.storeno = data3["storeno"].ToString();
                        model4.Batchcode.Add(model7);
                    }
                }
                item.Detail.Add(model4);
            }
            foreach (KeyValuePair<string, StockoutModel> pair in dictionary)
            {
                list.Add(pair.Value);
            }
            return model.toJson(list);
        }

        [HttpGet, Route("getckNoticeList")]
        public HttpResponseMessage getckNoticeList(string sourceid, int outtype, string datacode)
        {
            string outno = "";
            int back = 0;
            string msgback = "";
            StockoutModel model = new StockoutModel();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            StockOutBO.cknoticeScanf(sourceid, outtype, datacode, userByPk.Code, ref outno, ref back, ref msgback);
            return this.getckNoticeList(sourceid, outtype);
        }

        [HttpGet, Route("getckNoticeList")]
        public HttpResponseMessage getckNoticeList(string sourceid, int outtype, string datacode, string outno)
        {
            StockoutModel model = new StockoutModel();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            StockOutBO.cknoticedeldatacode(sourceid, outtype, outno, datacode, userByPk.Code);
            return this.getckNoticeList(sourceid, outtype);
        }

        [HttpGet, Route("outstoretallying")]
        public HttpResponseMessage outstoretallying(int isscanf, int outtype, string outno, string datacode)
        {
            List<DBData> list = new List<DBData>();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            return HttpHelper.toJson(OutStoretallyingBo.outstoretallying(isscanf, outtype, outno, datacode, userByPk.Code));
        }
    }
}

