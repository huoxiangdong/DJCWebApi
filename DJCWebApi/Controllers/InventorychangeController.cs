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

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/inventorychange")]
    public class InventorychangeController : ApiController
    {
        [HttpGet, Route("checkstore")]
        public HttpResponseMessage checkstore(string storageno) => 
            HttpHelper.toJson(StockInBO.checkstore(storageno));

        public HttpResponseMessage Get() => 
            new HttpResponseMessage { Content = new StringContent("value", Encoding.UTF8, "application/json") };

        [HttpGet, Route("getinvenchgrecord")]
        public HttpResponseMessage getinvenchgrecordbycode(string barcode, string inno)
        {
            List<PRoStockinBarcodeModel> list = new List<PRoStockinBarcodeModel>();
            PRoStockinBarcodeModel model = new PRoStockinBarcodeModel();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockInBO.inventorychange_bybarcode(barcode, userByPk.Code, inno);
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

        [HttpGet, Route("postinvenchgrecord")]
        public HttpResponseMessage postinvenchgrecord(string incode, string storageno)
        {
            List<PRoStockinInno> list = new List<PRoStockinInno>();
            PRoStockinInno inno = new PRoStockinInno();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            List<DBData> list2 = StockInBO.inventorychangepost_bybarcode(incode, storageno);
            foreach (DBData data in list2)
            {
                PRoStockinInno item = new PRoStockinInno {
                    Inno = data["inno"].ToString()
                };
                list.Add(item);
            }
            return inno.toJson(list);
        }
    }
}

