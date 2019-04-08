namespace DJCWebApi.Providers
{
    using Microsoft.Owin.Security.Infrastructure;
    using System;
    using System.Collections.Concurrent;

    public class OpenAuthorizationCodeProvider : AuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> _authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            this._authenticationCodes[context.Token] = context.SerializeTicket();
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            if (this._authenticationCodes.TryRemove(context.Token, out string str))
            {
                context.DeserializeTicket(str);
            }
        }
    }
}

