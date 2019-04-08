namespace DJCWebApi.Controllers
{
    using DJCWebApi.ws.KB.DataCollecters;
    using PI.ws;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;

    [RoutePrefix("api/publisher")]
    public class PublisherController : ApiController
    {
        public HttpResponseMessage Get()
        {
            DemoPublisher puber = new DemoPublisher {
                Identity = "puber"
            };
            WSPublisherManager.PublisherManager.AddPublisher(puber);
            puber.Start();
            return new HttpResponseMessage { Content = new StringContent("Publisher was Started...", Encoding.UTF8, "application/json") };
        }
    }
}

