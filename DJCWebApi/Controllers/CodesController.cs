namespace DJCWebApi.Controllers
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    public class CodesController : ApiController
    {
        [HttpGet, Route("api/authorization_code")]
        public HttpResponseMessage Get(string code) => 
            new HttpResponseMessage { Content = new StringContent(code, Encoding.UTF8, "text/plain") };
    }
}

