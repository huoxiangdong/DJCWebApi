namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Net.Http.Headers;
    using System.Web.Http;

    public static class HelpPageConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SetSampleForMediaType(new TextSample("Binary JSON content. See http://bsonspec.org for details."), new MediaTypeHeaderValue("application/bson"));
        }
    }
}

