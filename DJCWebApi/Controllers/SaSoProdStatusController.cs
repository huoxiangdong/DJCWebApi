namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Models.KCInventory;
    using DJCWebApiBO.KB;
    using DJCWebApiBO.User;
    using PI.Core.DA;
    using PI.Core.vo;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/sasoprodstatus")]
    public class SaSoProdStatusController : ApiController
    {
        private List<SaSoCusModels> convertcusToPicker(List<DBData> cuslist)
        {
            List<SaSoCusModels> list = new List<SaSoCusModels>();
            Dictionary<string, SaSoCusModels> dictionary = new Dictionary<string, SaSoCusModels>();
            foreach (DBData data in cuslist)
            {
                SaSoCusModels models = new SaSoCusModels();
                if (!dictionary.ContainsKey(data["pfix"].ToString()))
                {
                    models.value = data["pfix"].ToString();
                    models.label = data["pfix"].ToString();
                    models.children = new List<SaSoCus2Models>();
                    SaSoCus2Models item = new SaSoCus2Models {
                        value = data["custno"].ToString(),
                        label = data["custno"].ToString(),
                        children = new List<SaSoModels>()
                    };
                    SaSoModels models3 = new SaSoModels {
                        value = data["orderno"].ToString(),
                        label = data["orderno"].ToString()
                    };
                    item.children.Add(models3);
                    models.children.Add(item);
                    dictionary.Add(data["pfix"].ToString(), models);
                    continue;
                }
                dictionary.TryGetValue(data["pfix"].ToString(), out models);
                bool flag2 = false;
                foreach (SaSoCus2Models models4 in models.children)
                {
                    if (data["custno"].ToString() == models4.value)
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        SaSoModels item = new SaSoModels {
                            value = data["orderno"].ToString(),
                            label = data["orderno"].ToString()
                        };
                        models4.children.Add(item);
                        break;
                    }
                }
                if (!flag2)
                {
                    SaSoCus2Models item = new SaSoCus2Models {
                        value = data["custno"].ToString(),
                        label = data["custno"].ToString(),
                        children = new List<SaSoModels>()
                    };
                    SaSoModels models7 = new SaSoModels {
                        value = data["orderno"].ToString(),
                        label = data["orderno"].ToString()
                    };
                    item.children.Add(models7);
                    models.children.Add(item);
                }
            }
            foreach (KeyValuePair<string, SaSoCusModels> pair in dictionary)
            {
                list.Add(pair.Value);
            }
            return list;
        }

        public HttpResponseMessage Get() => 
            new HttpResponseMessage { Content = new StringContent("value", Encoding.UTF8, "application/json") };

        [HttpGet, Route("getSaSoProdStatus")]
        public HttpResponseMessage getSaSoProdStatus(string so, string cus)
        {
            SaSolist solist = new SaSolist();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            DBData data = new DBData();
            List<SaSolist> list = this.getsomolsit(SaSoProdBO.rpt_getSaSoProStatus(so));
            List<SaSoCusModels> list2 = this.convertcusToPicker(SaSoProdBO.rpt_getSaSoCuslist());
            List<DBData> list3 = SaSoProdBO.rpt_getSaSoStatus(cus);
            data.Add("sasoprodstatus", list);
            data.Add("cuslist", list2);
            data.Add("sasostatus", list3);
            return solist.toJson(data);
        }

        private List<SaSolist> getsomolsit(List<DBData> sasolist)
        {
            List<SaSolist> list = new List<SaSolist>();
            bool flag = false;
            foreach (DBData data in sasolist)
            {
                flag = false;
                foreach (SaSolist solist in list)
                {
                    if ((data["orderno"].ToString() == solist.orderno) && (data["autoid"].ToString() == solist.autoid))
                    {
                        flag = true;
                    }
                    if (flag && ("" != data["morderno"].ToString()))
                    {
                        SaSoProdStatus item = new SaSoProdStatus {
                            morderno = data["morderno"].ToString(),
                            momcode = data["momcode"].ToString(),
                            treeno = data["treeno"].ToString(),
                            ptreeno = data["ptreeno"].ToString(),
                            protype = data["protype"].ToString(),
                            moqty = Convert.ToDecimal(data["moqty"]),
                            jweigh = Convert.ToDecimal(data["jweigh"]),
                            rkqty = Convert.ToDecimal(data["kcoutqty"]),
                            mosprc = data["mosprc"].ToString(),
                            wofinrate = Convert.ToDecimal(data["wofinrate"])
                        };
                        solist.prodstatus.Add(item);
                        break;
                    }
                }
                if (!flag)
                {
                    SaSolist item = new SaSolist {
                        orderno = data["orderno"].ToString(),
                        orderdt = data["orderdt"].ToString(),
                        custno = data["custno"].ToString(),
                        pfix = data["pfix"].ToString(),
                        cusname = data["cusname"].ToString(),
                        corderno = data["corderno"].ToString(),
                        autoid = data["autoid"].ToString(),
                        mcode = data["mcode"].ToString(),
                        qty = Convert.ToDecimal(data["qty"]),
                        ckqty = Convert.ToDecimal(data["kcqty"]),
                        sprc = data["sprc"].ToString(),
                        sofinrate = Convert.ToDecimal(data["sofinrate"]),
                        prodstatus = new List<SaSoProdStatus>()
                    };
                    if ("" != data["morderno"].ToString())
                    {
                        SaSoProdStatus status2 = new SaSoProdStatus {
                            morderno = data["morderno"].ToString(),
                            momcode = data["momcode"].ToString(),
                            treeno = data["treeno"].ToString(),
                            ptreeno = data["ptreeno"].ToString(),
                            protype = data["protype"].ToString(),
                            moqty = Convert.ToDecimal(data["moqty"]),
                            jweigh = Convert.ToDecimal(data["jweigh"]),
                            rkqty = Convert.ToDecimal(data["kcoutqty"]),
                            mosprc = data["mosprc"].ToString(),
                            wofinrate = Convert.ToDecimal(data["wofinrate"])
                        };
                        item.prodstatus.Add(status2);
                    }
                    list.Add(item);
                }
            }
            return list;
        }
    }
}

