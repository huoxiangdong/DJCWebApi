namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Models;
    using DJCWebApi.Providers;
    using DJCWebApi.Results;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    [Authorize, RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ISecureDataFormat<AuthenticationTicket> <AccessTokenFormat>k__BackingField;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            this.UserManager = userManager;
            this.AccessTokenFormat = accessTokenFormat;
        }

        [AsyncStateMachine(typeof(<AddExternalLogin>d__16)), DebuggerStepThrough, Route("AddExternalLogin")]
        public Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            <AddExternalLogin>d__16 stateMachine = new <AddExternalLogin>d__16 {
                <>4__this = this,
                model = model,
                <>t__builder = AsyncTaskMethodBuilder<IHttpActionResult>.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<AddExternalLogin>d__16>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ChangePassword>d__14)), DebuggerStepThrough, Route("ChangePassword")]
        public Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            <ChangePassword>d__14 stateMachine = new <ChangePassword>d__14 {
                <>4__this = this,
                model = model,
                <>t__builder = AsyncTaskMethodBuilder<IHttpActionResult>.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<ChangePassword>d__14>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this._userManager > null))
            {
                this._userManager.Dispose();
                this._userManager = null;
            }
            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }
            if (!result.Succeeded)
            {
                if (result.Errors > null)
                {
                    foreach (string str in result.Errors)
                    {
                        base.ModelState.AddModelError("", str);
                    }
                }
                if (base.ModelState.IsValid)
                {
                    return this.BadRequest();
                }
                return this.BadRequest(base.ModelState);
            }
            return null;
        }

        [OverrideAuthentication, HostAuthentication("ExternalCookie"), AllowAnonymous, Route("ExternalLogin", Name="ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error > null)
            {
                return this.Redirect(this.Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }
            if (!this.User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(this.User.Identity as ClaimsIdentity);
            if (externalLogin == null)
            {
                return this.InternalServerError();
            }
            if (externalLogin.LoginProvider != provider)
            {
                string[] authenticationTypes = new string[] { "ExternalCookie" };
                this.Authentication.SignOut(authenticationTypes);
                return new ChallengeResult(provider, this);
            }
            ApplicationUser asyncVariable1 = await this.UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));
            ApplicationUser asyncVariable0 = asyncVariable1;
            asyncVariable1 = null;
            if (asyncVariable0 > null)
            {
                string[] authenticationTypes = new string[] { "ExternalCookie" };
                this.Authentication.SignOut(authenticationTypes);
                ClaimsIdentity identity = await asyncVariable0.GenerateUserIdentityAsync(this.UserManager, "Bearer");
                ClaimsIdentity asyncVariable2 = identity;
                ClaimsIdentity oAuthIdentity = asyncVariable2;
                asyncVariable2 = null;
                identity = await asyncVariable0.GenerateUserIdentityAsync(this.UserManager, "Cookies");
                ClaimsIdentity asyncVariable3 = identity;
                ClaimsIdentity cookieIdentity = asyncVariable3;
                asyncVariable3 = null;
                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(asyncVariable0);
                ClaimsIdentity[] identities = new ClaimsIdentity[] { oAuthIdentity, cookieIdentity };
                this.Authentication.SignIn(properties, identities);
                oAuthIdentity = null;
                cookieIdentity = null;
                properties = null;
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity asyncVariable4 = new ClaimsIdentity(claims, "Bearer");
                ClaimsIdentity[] identities = new ClaimsIdentity[] { asyncVariable4 };
                this.Authentication.SignIn(identities);
                claims = null;
                asyncVariable4 = null;
            }
            return this.Ok();
        }

        [AllowAnonymous, Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            string str;
            IEnumerable<AuthenticationDescription> externalAuthenticationTypes = this.Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> list = new List<ExternalLoginViewModel>();
            if (generateState)
            {
                str = RandomOAuthStateGenerator.Generate(0x100);
            }
            else
            {
                str = null;
            }
            foreach (AuthenticationDescription description in externalAuthenticationTypes)
            {
                ExternalLoginViewModel item = new ExternalLoginViewModel {
                    Name = description.Caption,
                    Url = base.Url.Route("ExternalLogin", new { 
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(base.Request.RequestUri, returnUrl).AbsoluteUri,
                        state = str
                    }),
                    State = str
                };
                list.Add(item);
            }
            return list;
        }

        [AsyncStateMachine(typeof(<GetManageInfo>d__13)), DebuggerStepThrough, Route("ManageInfo")]
        public Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            <GetManageInfo>d__13 stateMachine = new <GetManageInfo>d__13 {
                <>4__this = this,
                returnUrl = returnUrl,
                generateState = generateState,
                <>t__builder = AsyncTaskMethodBuilder<ManageInfoViewModel>.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<GetManageInfo>d__13>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [HostAuthentication("ExternalBearer"), Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData data = ExternalLoginData.FromIdentity(base.User.Identity as ClaimsIdentity);
            return new UserInfoViewModel { 
                Email = base.User.Identity.GetUserName(),
                HasRegistered = data == null,
                LoginProvider = data?.LoginProvider
            };
        }

        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            string[] authenticationTypes = new string[] { "Cookies" };
            this.Authentication.SignOut(authenticationTypes);
            return this.Ok();
        }

        [AsyncStateMachine(typeof(<Register>d__20)), DebuggerStepThrough, AllowAnonymous, Route("Register")]
        public Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            <Register>d__20 stateMachine = new <Register>d__20 {
                <>4__this = this,
                model = model,
                <>t__builder = AsyncTaskMethodBuilder<IHttpActionResult>.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<Register>d__20>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [OverrideAuthentication, HostAuthentication("ExternalBearer"), Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            IHttpActionResult errorResult;
            if (!this.ModelState.IsValid)
            {
                errorResult = this.BadRequest(this.ModelState);
            }
            else
            {
                ExternalLoginInfo asyncVariable3 = await this.Authentication.GetExternalLoginInfoAsync();
                ExternalLoginInfo asyncVariable0 = asyncVariable3;
                asyncVariable3 = null;
                if (asyncVariable0 == null)
                {
                    return this.InternalServerError();
                }
                ApplicationUser user = new ApplicationUser {
                    UserName = model.Email,
                    Email = model.Email
                };
                IdentityResult result2 = await this.UserManager.CreateAsync(user);
                IdentityResult asyncVariable4 = result2;
                IdentityResult result = asyncVariable4;
                asyncVariable4 = null;
                if (!result.Succeeded)
                {
                    errorResult = this.GetErrorResult(result);
                }
                else
                {
                    result2 = await this.UserManager.AddLoginAsync(user.Id, asyncVariable0.Login);
                    IdentityResult asyncVariable5 = result2;
                    result = asyncVariable5;
                    asyncVariable5 = null;
                    if (!result.Succeeded)
                    {
                        errorResult = this.GetErrorResult(result);
                    }
                    else
                    {
                        errorResult = this.Ok();
                    }
                }
            }
            return errorResult;
        }

        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            IHttpActionResult errorResult;
            IdentityResult result2;
            IdentityResult asyncVariable0;
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }
            if (model.LoginProvider == "Local")
            {
                result2 = await this.UserManager.RemovePasswordAsync(this.User.Identity.GetUserId());
                IdentityResult asyncVariable1 = result2;
                asyncVariable0 = asyncVariable1;
                asyncVariable1 = null;
            }
            else
            {
                result2 = await this.UserManager.RemoveLoginAsync(this.User.Identity.GetUserId(), new UserLoginInfo(model.LoginProvider, model.ProviderKey));
                IdentityResult asyncVariable2 = result2;
                asyncVariable0 = asyncVariable2;
                asyncVariable2 = null;
            }
            if (!asyncVariable0.Succeeded)
            {
                errorResult = this.GetErrorResult(asyncVariable0);
            }
            else
            {
                errorResult = this.Ok();
            }
            return errorResult;
        }

        [AsyncStateMachine(typeof(<SetPassword>d__15)), DebuggerStepThrough, Route("SetPassword")]
        public Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            <SetPassword>d__15 stateMachine = new <SetPassword>d__15 {
                <>4__this = this,
                model = model,
                <>t__builder = AsyncTaskMethodBuilder<IHttpActionResult>.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<SetPassword>d__15>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        public ApplicationUserManager UserManager
        {
            get => 
                (this._userManager ?? base.Request.GetOwinContext().GetUserManager<ApplicationUserManager>());
            private set => 
                (this._userManager = value);
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        private IAuthenticationManager Authentication =>
            base.Request.GetOwinContext().Authentication;

        [CompilerGenerated]
        private sealed class <AddExternalLogin>d__16 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IHttpActionResult> <>t__builder;
            public AddExternalLoginBindingModel model;
            public AccountController <>4__this;
            private AuthenticationTicket <ticket>5__1;
            private AccountController.ExternalLoginData <externalData>5__2;
            private IdentityResult <result>5__3;
            private IdentityResult <>s__4;
            private TaskAwaiter<IdentityResult> <>u__1;

            private void MoveNext()
            {
                IHttpActionResult errorResult;
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IdentityResult> awaiter;
                    IdentityResult result2;
                    if (num != 0)
                    {
                        if (this.<>4__this.ModelState.IsValid)
                        {
                            string[] authenticationTypes = new string[] { "ExternalCookie" };
                            this.<>4__this.Authentication.SignOut(authenticationTypes);
                            this.<ticket>5__1 = this.<>4__this.AccessTokenFormat.Unprotect(this.model.ExternalAccessToken);
                            if (((this.<ticket>5__1 != null) && (this.<ticket>5__1.Identity != null)) && (((this.<ticket>5__1.Properties == null) || !this.<ticket>5__1.Properties.ExpiresUtc.HasValue) || (this.<ticket>5__1.Properties.ExpiresUtc.Value >= DateTimeOffset.UtcNow)))
                            {
                                this.<externalData>5__2 = AccountController.ExternalLoginData.FromIdentity(this.<ticket>5__1.Identity);
                                if (this.<externalData>5__2 != null)
                                {
                                    awaiter = this.<>4__this.UserManager.AddLoginAsync(this.<>4__this.User.Identity.GetUserId(), new UserLoginInfo(this.<externalData>5__2.LoginProvider, this.<externalData>5__2.ProviderKey)).GetAwaiter();
                                    if (!awaiter.IsCompleted)
                                    {
                                        this.<>1__state = num = 0;
                                        this.<>u__1 = awaiter;
                                        AccountController.<AddExternalLogin>d__16 stateMachine = this;
                                        this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IdentityResult>, AccountController.<AddExternalLogin>d__16>(ref awaiter, ref stateMachine);
                                        return;
                                    }
                                    goto Label_01D9;
                                }
                                errorResult = this.<>4__this.BadRequest("The external login is already associated with an account.");
                            }
                            else
                            {
                                errorResult = this.<>4__this.BadRequest("External login failure.");
                            }
                        }
                        else
                        {
                            errorResult = this.<>4__this.BadRequest(this.<>4__this.ModelState);
                        }
                        goto Label_0256;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter<IdentityResult>();
                    this.<>1__state = num = -1;
                Label_01D9:
                    result2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<IdentityResult>();
                    this.<>s__4 = result2;
                    this.<result>5__3 = this.<>s__4;
                    this.<>s__4 = null;
                    if (!this.<result>5__3.Succeeded)
                    {
                        errorResult = this.<>4__this.GetErrorResult(this.<result>5__3);
                    }
                    else
                    {
                        errorResult = this.<>4__this.Ok();
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_0256:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(errorResult);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        [CompilerGenerated]
        private sealed class <ChangePassword>d__14 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IHttpActionResult> <>t__builder;
            public ChangePasswordBindingModel model;
            public AccountController <>4__this;
            private IdentityResult <result>5__1;
            private IdentityResult <>s__2;
            private TaskAwaiter<IdentityResult> <>u__1;

            private void MoveNext()
            {
                IHttpActionResult errorResult;
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IdentityResult> awaiter;
                    IdentityResult result2;
                    if (num != 0)
                    {
                        if (this.<>4__this.ModelState.IsValid)
                        {
                            awaiter = this.<>4__this.UserManager.ChangePasswordAsync(this.<>4__this.User.Identity.GetUserId(), this.model.OldPassword, this.model.NewPassword).GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                AccountController.<ChangePassword>d__14 stateMachine = this;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IdentityResult>, AccountController.<ChangePassword>d__14>(ref awaiter, ref stateMachine);
                                return;
                            }
                            goto Label_00D4;
                        }
                        errorResult = this.<>4__this.BadRequest(this.<>4__this.ModelState);
                        goto Label_0151;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter<IdentityResult>();
                    this.<>1__state = num = -1;
                Label_00D4:
                    result2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<IdentityResult>();
                    this.<>s__2 = result2;
                    this.<result>5__1 = this.<>s__2;
                    this.<>s__2 = null;
                    if (!this.<result>5__1.Succeeded)
                    {
                        errorResult = this.<>4__this.GetErrorResult(this.<result>5__1);
                    }
                    else
                    {
                        errorResult = this.<>4__this.Ok();
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_0151:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(errorResult);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }


        [CompilerGenerated]
        private sealed class <GetManageInfo>d__13 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<ManageInfoViewModel> <>t__builder;
            public string returnUrl;
            public bool generateState;
            public AccountController <>4__this;
            private IdentityUser <user>5__1;
            private List<UserLoginInfoViewModel> <logins>5__2;
            private ApplicationUser <>s__3;
            private IEnumerator<IdentityUserLogin> <>s__4;
            private IdentityUserLogin <linkedAccount>5__5;
            private TaskAwaiter<ApplicationUser> <>u__1;

            private void MoveNext()
            {
                ManageInfoViewModel model;
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<ApplicationUser> awaiter;
                    if (num != 0)
                    {
                        awaiter = this.<>4__this.UserManager.FindByIdAsync(this.<>4__this.User.Identity.GetUserId()).GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            AccountController.<GetManageInfo>d__13 stateMachine = this;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ApplicationUser>, AccountController.<GetManageInfo>d__13>(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<ApplicationUser>();
                        this.<>1__state = num = -1;
                    }
                    ApplicationUser result = awaiter.GetResult();
                    awaiter = new TaskAwaiter<ApplicationUser>();
                    this.<>s__3 = result;
                    this.<user>5__1 = this.<>s__3;
                    this.<>s__3 = null;
                    if (this.<user>5__1 == null)
                    {
                        model = null;
                    }
                    else
                    {
                        UserLoginInfoViewModel model2;
                        this.<logins>5__2 = new List<UserLoginInfoViewModel>();
                        this.<>s__4 = this.<user>5__1.Logins.GetEnumerator();
                        try
                        {
                            while (this.<>s__4.MoveNext())
                            {
                                this.<linkedAccount>5__5 = this.<>s__4.Current;
                                model2 = new UserLoginInfoViewModel {
                                    LoginProvider = this.<linkedAccount>5__5.LoginProvider,
                                    ProviderKey = this.<linkedAccount>5__5.ProviderKey
                                };
                                this.<logins>5__2.Add(model2);
                                this.<linkedAccount>5__5 = null;
                            }
                        }
                        finally
                        {
                            if ((num < 0) && (this.<>s__4 != null))
                            {
                                this.<>s__4.Dispose();
                            }
                        }
                        this.<>s__4 = null;
                        if (this.<user>5__1.PasswordHash > null)
                        {
                            model2 = new UserLoginInfoViewModel {
                                LoginProvider = "Local",
                                ProviderKey = this.<user>5__1.UserName
                            };
                            this.<logins>5__2.Add(model2);
                        }
                        model = new ManageInfoViewModel {
                            LocalLoginProvider = "Local",
                            Email = this.<user>5__1.UserName,
                            Logins = this.<logins>5__2,
                            ExternalLoginProviders = this.<>4__this.GetExternalLogins(this.returnUrl, this.generateState)
                        };
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult(model);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        [CompilerGenerated]
        private sealed class <Register>d__20 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IHttpActionResult> <>t__builder;
            public RegisterBindingModel model;
            public AccountController <>4__this;
            private ApplicationUser <user>5__1;
            private IdentityResult <result>5__2;
            private IdentityResult <>s__3;
            private TaskAwaiter<IdentityResult> <>u__1;

            private void MoveNext()
            {
                IHttpActionResult errorResult;
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IdentityResult> awaiter;
                    IdentityResult result2;
                    if (num != 0)
                    {
                        if (this.<>4__this.ModelState.IsValid)
                        {
                            ApplicationUser user = new ApplicationUser {
                                UserName = this.model.Email,
                                Email = this.model.Email
                            };
                            this.<user>5__1 = user;
                            awaiter = this.<>4__this.UserManager.CreateAsync(this.<user>5__1, this.model.Password).GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                AccountController.<Register>d__20 stateMachine = this;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IdentityResult>, AccountController.<Register>d__20>(ref awaiter, ref stateMachine);
                                return;
                            }
                            goto Label_00EE;
                        }
                        errorResult = this.<>4__this.BadRequest(this.<>4__this.ModelState);
                        goto Label_016B;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter<IdentityResult>();
                    this.<>1__state = num = -1;
                Label_00EE:
                    result2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<IdentityResult>();
                    this.<>s__3 = result2;
                    this.<result>5__2 = this.<>s__3;
                    this.<>s__3 = null;
                    if (!this.<result>5__2.Succeeded)
                    {
                        errorResult = this.<>4__this.GetErrorResult(this.<result>5__2);
                    }
                    else
                    {
                        errorResult = this.<>4__this.Ok();
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_016B:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(errorResult);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }



        [CompilerGenerated]
        private sealed class <SetPassword>d__15 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder<IHttpActionResult> <>t__builder;
            public SetPasswordBindingModel model;
            public AccountController <>4__this;
            private IdentityResult <result>5__1;
            private IdentityResult <>s__2;
            private TaskAwaiter<IdentityResult> <>u__1;

            private void MoveNext()
            {
                IHttpActionResult errorResult;
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<IdentityResult> awaiter;
                    IdentityResult result2;
                    if (num != 0)
                    {
                        if (this.<>4__this.ModelState.IsValid)
                        {
                            awaiter = this.<>4__this.UserManager.AddPasswordAsync(this.<>4__this.User.Identity.GetUserId(), this.model.NewPassword).GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                AccountController.<SetPassword>d__15 stateMachine = this;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IdentityResult>, AccountController.<SetPassword>d__15>(ref awaiter, ref stateMachine);
                                return;
                            }
                            goto Label_00C9;
                        }
                        errorResult = this.<>4__this.BadRequest(this.<>4__this.ModelState);
                        goto Label_0146;
                    }
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter<IdentityResult>();
                    this.<>1__state = num = -1;
                Label_00C9:
                    result2 = awaiter.GetResult();
                    awaiter = new TaskAwaiter<IdentityResult>();
                    this.<>s__2 = result2;
                    this.<result>5__1 = this.<>s__2;
                    this.<>s__2 = null;
                    if (!this.<result>5__1.Succeeded)
                    {
                        errorResult = this.<>4__this.GetErrorResult(this.<result>5__1);
                    }
                    else
                    {
                        errorResult = this.<>4__this.Ok();
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
            Label_0146:
                this.<>1__state = -2;
                this.<>t__builder.SetResult(errorResult);
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        private class ExternalLoginData
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <LoginProvider>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <ProviderKey>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <UserName>k__BackingField;

            public static AccountController.ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }
                Claim claim = identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (((claim == null) || string.IsNullOrEmpty(claim.Issuer)) || string.IsNullOrEmpty(claim.Value))
                {
                    return null;
                }
                if (claim.Issuer == "LOCAL AUTHORITY")
                {
                    return null;
                }
                return new AccountController.ExternalLoginData { 
                    LoginProvider = claim.Issuer,
                    ProviderKey = claim.Value,
                    UserName = identity.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                };
            }

            public IList<Claim> GetClaims()
            {
                IList<Claim> list = new List<Claim> {
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", this.ProviderKey, null, this.LoginProvider)
                };
                if (this.UserName > null)
                {
                    list.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", this.UserName, null, this.LoginProvider));
                }
                return list;
            }

            public string LoginProvider { get; set; }

            public string ProviderKey { get; set; }

            public string UserName { get; set; }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                if ((strengthInBits % 8) != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }
                int num = strengthInBits / 8;
                byte[] data = new byte[num];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }
    }
}

