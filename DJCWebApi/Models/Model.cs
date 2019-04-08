namespace DJCWebApi.Models
{
    using DJCWebApi.Utils;
    using System;
    using System.Net.Http;

    public class Model
    {
        public HttpResponseMessage toJson(object obj) => 
            HttpHelper.toJson(obj);
    }
}

