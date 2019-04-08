namespace DJCWebApi.Providers
{
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Infrastructure;
    using Microsoft.Owin.Security.OAuth;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    public class OpenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        [CompilerGenerated]
        private Task <>n__0(OAuthGrantResourceOwnerCredentialsContext context) => 
            base.GrantResourceOwnerCredentials(context);

        [AsyncStateMachine(typeof(<AuthorizeEndpoint>d__2)), DebuggerStepThrough]
        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            <AuthorizeEndpoint>d__2 stateMachine = new <AuthorizeEndpoint>d__2 {
                <>4__this = this,
                context = context,
                <>t__builder = AsyncTaskMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<AuthorizeEndpoint>d__2>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<GrantResourceOwnerCredentials>d__1)), DebuggerStepThrough]
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            <GrantResourceOwnerCredentials>d__1 stateMachine = new <GrantResourceOwnerCredentials>d__1 {
                <>4__this = this,
                context = context,
                <>t__builder = AsyncTaskMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<GrantResourceOwnerCredentials>d__1>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ValidateAuthorizeRequest>d__3)), DebuggerStepThrough]
        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            <ValidateAuthorizeRequest>d__3 stateMachine = new <ValidateAuthorizeRequest>d__3 {
                <>4__this = this,
                context = context,
                <>t__builder = AsyncTaskMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<ValidateAuthorizeRequest>d__3>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ValidateClientAuthentication>d__0)), DebuggerStepThrough]
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            <ValidateClientAuthentication>d__0 stateMachine = new <ValidateClientAuthentication>d__0 {
                <>4__this = this,
                context = context,
                <>t__builder = AsyncTaskMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<ValidateClientAuthentication>d__0>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        public async override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            context.Validated(context.RedirectUri);
        }

        [AsyncStateMachine(typeof(<ValidateTokenRequest>d__5)), DebuggerStepThrough]
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            <ValidateTokenRequest>d__5 stateMachine = new <ValidateTokenRequest>d__5 {
                <>4__this = this,
                context = context,
                <>t__builder = AsyncTaskMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<ValidateTokenRequest>d__5>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [CompilerGenerated]
        private sealed class <AuthorizeEndpoint>d__2 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public OAuthAuthorizeEndpointContext context;
            public OpenAuthorizationServerProvider <>4__this;
            private ClaimsIdentity <identity>5__1;
            private string <redirectUri>5__2;
            private string <clientId>5__3;
            private ClaimsIdentity <identity>5__4;
            private AuthenticationTokenCreateContext <authorizeCodeContext>5__5;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        if (!this.context.AuthorizeRequest.IsImplicitGrantType)
                        {
                            if (this.context.AuthorizeRequest.IsAuthorizationCodeGrantType)
                            {
                                this.<redirectUri>5__2 = this.context.Request.Query["redirect_uri"];
                                this.<clientId>5__3 = this.context.Request.Query["client_id"];
                                this.<identity>5__4 = new ClaimsIdentity(new GenericIdentity(this.<clientId>5__3, "Bearer"));
                                Dictionary<string, string> dictionary = new Dictionary<string, string> {
                                    { 
                                        "client_id",
                                        this.<clientId>5__3
                                    },
                                    { 
                                        "redirect_uri",
                                        this.<redirectUri>5__2
                                    }
                                };
                                AuthenticationProperties properties = new AuthenticationProperties(dictionary) {
                                    IssuedUtc = new DateTimeOffset?(DateTimeOffset.UtcNow),
                                    ExpiresUtc = new DateTimeOffset?(DateTimeOffset.UtcNow.Add(this.context.Options.AuthorizationCodeExpireTimeSpan))
                                };
                                this.<authorizeCodeContext>5__5 = new AuthenticationTokenCreateContext(this.context.OwinContext, this.context.Options.AuthorizationCodeFormat, new AuthenticationTicket(this.<identity>5__4, properties));
                                awaiter = this.context.Options.AuthorizationCodeProvider.CreateAsync(this.<authorizeCodeContext>5__5).GetAwaiter();
                                if (!awaiter.IsCompleted)
                                {
                                    this.<>1__state = num = 0;
                                    this.<>u__1 = awaiter;
                                    OpenAuthorizationServerProvider.<AuthorizeEndpoint>d__2 stateMachine = this;
                                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, OpenAuthorizationServerProvider.<AuthorizeEndpoint>d__2>(ref awaiter, ref stateMachine);
                                    return;
                                }
                                goto Label_01FB;
                            }
                        }
                        else
                        {
                            this.<identity>5__1 = new ClaimsIdentity("Bearer");
                            ClaimsIdentity[] identities = new ClaimsIdentity[] { this.<identity>5__1 };
                            this.context.OwinContext.Authentication.SignIn(identities);
                            this.context.RequestCompleted();
                            this.<identity>5__1 = null;
                        }
                        goto Label_0281;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter();
                    this.<>1__state = num = -1;
                Label_01FB:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.context.Response.Redirect(this.<redirectUri>5__2 + "?code=" + Uri.EscapeDataString(this.<authorizeCodeContext>5__5.Token));
                    this.context.RequestCompleted();
                    this.<redirectUri>5__2 = null;
                    this.<clientId>5__3 = null;
                    this.<identity>5__4 = null;
                    this.<authorizeCodeContext>5__5 = null;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_0281:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        [CompilerGenerated]
        private sealed class <GrantResourceOwnerCredentials>d__1 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public OAuthGrantResourceOwnerCredentialsContext context;
            public OpenAuthorizationServerProvider <>4__this;
            private ClaimsIdentity <oAuthIdentity>5__1;
            private AuthenticationTicket <ticket>5__2;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        if ((this.context.UserName == "Admin") && (this.context.Password == "123456"))
                        {
                            this.<oAuthIdentity>5__1 = new ClaimsIdentity(this.context.Options.AuthenticationType);
                            this.<oAuthIdentity>5__1.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", this.context.UserName));
                            this.<ticket>5__2 = new AuthenticationTicket(this.<oAuthIdentity>5__1, new AuthenticationProperties());
                            this.context.Validated(this.<ticket>5__2);
                            awaiter = this.<>4__this.<>n__0(this.context).GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                OpenAuthorizationServerProvider.<GrantResourceOwnerCredentials>d__1 stateMachine = this;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, OpenAuthorizationServerProvider.<GrantResourceOwnerCredentials>d__1>(ref awaiter, ref stateMachine);
                                return;
                            }
                            goto Label_0125;
                        }
                        this.context.SetError("invalid_grant", "用户名或密码不正确。");
                        goto Label_0151;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter();
                    this.<>1__state = num = -1;
                Label_0125:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_0151:
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        [CompilerGenerated]
        private sealed class <ValidateAuthorizeRequest>d__3 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public OAuthValidateAuthorizeRequestContext context;
            public OpenAuthorizationServerProvider <>4__this;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if ((this.context.AuthorizeRequest.ClientId == "xishuai") && (this.context.AuthorizeRequest.IsAuthorizationCodeGrantType || this.context.AuthorizeRequest.IsImplicitGrantType))
                    {
                        this.context.Validated();
                    }
                    else
                    {
                        this.context.Rejected();
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        [CompilerGenerated]
        private sealed class <ValidateClientAuthentication>d__0 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public OAuthValidateClientAuthenticationContext context;
            public OpenAuthorizationServerProvider <>4__this;
            private string <clientId>5__1;
            private string <clientSecret>5__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (!this.context.TryGetBasicCredentials(out this.<clientId>5__1, out this.<clientSecret>5__2))
                    {
                        this.context.TryGetFormCredentials(out this.<clientId>5__1, out this.<clientSecret>5__2);
                    }
                    if (this.<clientId>5__1 != "xishuai")
                    {
                        this.context.SetError("invalid_client", "client is not valid");
                    }
                    else
                    {
                        this.context.Validated();
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }


        [CompilerGenerated]
        private sealed class <ValidateTokenRequest>d__5 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public OAuthValidateTokenRequestContext context;
            public OpenAuthorizationServerProvider <>4__this;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    if (this.context.TokenRequest.IsAuthorizationCodeGrantType || this.context.TokenRequest.IsRefreshTokenGrantType)
                    {
                        this.context.Validated();
                    }
                    else
                    {
                        this.context.Rejected();
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }
    }
}

