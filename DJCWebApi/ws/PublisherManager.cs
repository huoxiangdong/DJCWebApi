namespace DJCWebApi.ws
{
    using DJCWebApi.ws.KB.DataCollecters;
    using PI.ws;
    using System;

    public class PublisherManager
    {
        public static void AutoInstallPublisher()
        {
            KBPublisher puber = new KBPublisher {
                Identity = "kbpublisher"
            };
            InstallPublisher(puber);
        }

        public static void init()
        {
            WSPublisherManager.PublisherManager.URL = "ws://localhost:15850/api/WSChat";
        }

        public static void InstallPublisher(Publisher puber)
        {
            WSPublisherManager.PublisherManager.AddPublisher(puber);
            WSPublisherManager.PublisherManager.Start(puber);
        }
    }
}

