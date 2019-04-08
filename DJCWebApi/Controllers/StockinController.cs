namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Models.stockin;
    using DJCWebApi.Utils;
    using DJCWebApiBO.StockIn;
    using DJCWebApiBO.User;
    using PI.Core.DA;
    using PI.Core.vo;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/stockinpro")]
    public class StockinController : ApiController
    {
        [HttpGet, Route("checkstore")]
        public HttpResponseMessage checkstore(string storageno) => 
            HttpHelper.toJson(StockInBO.checkstore(storageno));

        public HttpResponseMessage Get() => 
            new HttpResponseMessage { Content = new StringContent("value", Encoding.UTF8, "application/json") };

        [HttpGet, Route("getrkbycodeinstore")]
        public HttpResponseMessage getrkbycodeinstore(string incode, string storageno)
        {
            List<PRoStockinInno> list = new List<PRoStockinInno>();
            PRoStockinInno inno = new PRoStockinInno();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockInBO.rkprodintstore_bybarcode(incode, storageno);
            foreach (DBData data in list2)
            {
                PRoStockinInno item = new PRoStockinInno {
                    Inno = data["inno"].ToString()
                };
                list.Add(item);
            }
            return inno.toJson(list);
        }

        [HttpGet, Route("getrkkabanlistbycode")]
        public HttpResponseMessage getrkkabanlistbycode(string barcode)
        {
            List<PRoStockinKabanModel> list = new List<PRoStockinKabanModel>();
            PRoStockinKabanModel model = new PRoStockinKabanModel();
            List<DBData> list2 = StockInBO.rkgetinstorelist_kabanbybarcodeQuery(barcode);
            foreach (DBData data in list2)
            {
                PRoStockinKabanModel item = new PRoStockinKabanModel {
                    Custno = data["custno"].ToString(),
                    Mcode = data["mcode"].ToString(),
                    Mname = data["mname"].ToString(),
                    Qty = Convert.ToDecimal(data["qty"]),
                    Storageno = data["storageno"].ToString(),
                    Zhoushu = Convert.ToInt16(data["zhoushu"]),
                    Barcode = data["barcode"].ToString()
                };
                list.Add(item);
            }
            return model.toJson(list);
        }

        [HttpGet, Route("getrkkabanlistinstore")]
        public HttpResponseMessage getrkkabanlistinstore(string barcode, string storageno)
        {
            string str = "";
            List<PRoStockinInno> list = new List<PRoStockinInno>();
            PRoStockinInno inno = new PRoStockinInno();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockInBO.rkprodintstore_kabanbybarcode(ref str, barcode, storageno, userByPk.Code);
            foreach (DBData data in list2)
            {
                PRoStockinInno item = new PRoStockinInno {
                    Inno = data["inno"].ToString()
                };
                list.Add(item);
            }
            return inno.toJson(list);
        }

        [HttpGet, Route("getrkpurlistbycode")]
        public HttpResponseMessage getrkpurlistbycode(string barcode)
        {
            string inno = "";
            List<PurStockinModel> list = new List<PurStockinModel>();
            PurStockinModel model = new PurStockinModel();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockInBO.rkgetinstorelist_weighbybarcodeQuery(ref inno, barcode, userByPk.Code);
            foreach (DBData data in list2)
            {
                PurStockinModel item = new PurStockinModel {
                    custno = data["batchno"].ToString(),
                    mcode = data["mcode"].ToString(),
                    mname = data["mname"].ToString(),
                    qty = Convert.ToDecimal(data["jweigh"]),
                    storageno = data["storageno"].ToString(),
                    zhoushu = Convert.ToInt16(data["qtyelse"]),
                    barcode = data["datacode"].ToString(),
                    zhoubatch = data["coilno"].ToString(),
                    purqty = Convert.ToDecimal(data["jweigh"]),
                    coptypeno = data["copno"].ToString()
                };
                list.Add(item);
            }
            return model.toJson(list);
        }

        [HttpGet, Route("getrkpurlistinstore")]
        public HttpResponseMessage getrkpurlistinstore(string barcode)
        {
            string str = "";
            List<PRoStockinInno> list = new List<PRoStockinInno>();
            PRoStockinInno inno = new PRoStockinInno();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockInBO.rkpurintstore_weighbybarcode(ref str, barcode, userByPk.Code);
            foreach (DBData data in list2)
            {
                PRoStockinInno item = new PRoStockinInno {
                    Inno = data["inno"].ToString()
                };
                list.Add(item);
            }
            return inno.toJson(list);
        }

        [HttpGet, Route("getrkrecordbycode")]
        public HttpResponseMessage getrkrecordbycode(string type, string barcode, string inno)
        {
            List<PRoStockinBarcodeModel> list = new List<PRoStockinBarcodeModel>();
            PRoStockinBarcodeModel model = new PRoStockinBarcodeModel();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockInBO.rkprodintstore_bybarcode(type, barcode, userByPk.Code, inno);
            foreach (DBData data in list2)
            {
                PRoStockinBarcodeModel item = new PRoStockinBarcodeModel {
                    Barcode = data["barcode"].ToString(),
                    Custno = data["custno"].ToString(),
                    Inno = data["inno"].ToString(),
                    Mcode = data["mcode"].ToString(),
                    Mname = data["mname"].ToString(),
                    Qty = Convert.ToDecimal(data["qty"]),
                    Storageno = data["storageno"].ToString(),
                    Zhoushu = Convert.ToInt16(data["zhoushu"])
                };
                list.Add(item);
            }
            return model.toJson(list);
        }
    }
}

