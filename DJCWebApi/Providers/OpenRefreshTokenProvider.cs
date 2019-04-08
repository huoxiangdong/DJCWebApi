namespace DJCWebApi.Providers
{
    using Microsoft.Owin.Security.Infrastructure;
    using System;
    using System.Collections.Concurrent;

    public class OpenRefreshTokenProvider : AuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, string> _refreshTokens = new ConcurrentDictionary<string, string>();

        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.IssuedUtc = new DateTimeOffset?(DateTime.UtcNow);
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset?(DateTime.UtcNow.AddDays(60.0));
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _refreshTokens[context.Token] = context.SerializeTicket();
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            if (_refreshTokens.TryRemove(context.Token, out string str))
            {
                context.DeserializeTicket(str);
            }
        }
    }
}

