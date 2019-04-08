namespace DJCWebApi.Utils
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Web.Script.Serialization;

    public class HttpHelper
    {
        public static HttpResponseMessage toJson(object obj)
        {
            string str;
            if ((obj is string) || (obj is char))
            {
                str = obj.ToString();
            }
            else
            {
                str = new JavaScriptSerializer { MaxJsonLength = 0x7fffffff }.Serialize(obj);
            }
            return new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
        }
    }
}

