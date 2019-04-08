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

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/precentachieve")]
    public class PrecentachieveController : ApiController
    {
        private List<SacuslistModels> convertcusToPicker(List<DBData> cuslist)
        {
            List<SacuslistModels> list = new List<SacuslistModels>();
            Dictionary<string, SacuslistModels> dictionary = new Dictionary<string, SacuslistModels>();
            foreach (DBData data in cuslist)
            {
                SacuslistModels models = new SacuslistModels();
                if (!dictionary.ContainsKey(data["pfix"].ToString()))
                {
                    models.value = data["pfix"].ToString();
                    models.label = data["pfix"].ToString();
                    dictionary.Add(data["pfix"].ToString(), models);
                    dictionary[data["pfix"].ToString()].children = new List<Sacuslist2Models>();
                }
                dictionary.TryGetValue(data["pfix"].ToString(), out models);
                Sacuslist2Models item = new Sacuslist2Models {
                    value = data["value"].ToString(),
                    label = data["label"].ToString()
                };
                models.children.Add(item);
            }
            foreach (KeyValuePair<string, SacuslistModels> pair in dictionary)
            {
                list.Add(pair.Value);
            }
            return list;
        }

        public HttpResponseMessage Get() => 
            new HttpResponseMessage { Content = new StringContent("value", Encoding.UTF8, "application/json") };

        [HttpGet, Route("getSoStateList")]
        public HttpResponseMessage getSoStateList(string cus)
        {
            PrecentachieveModels models = new PrecentachieveModels();
            UserVO userByPk = UserBO.GetUserByPk(base.User.Identity.Name);
            DBData data = new DBData();
            List<DBData> list = Precentachieve.rpt_getSaPercentachieve(cus);
            List<SacuslistModels> list2 = this.convertcusToPicker(Precentachieve.rpt_getSaCuslist());
            data.Add("percentachieve", list);
            data.Add("cuslist", list2);
            return models.toJson(data);
        }
    }
}

