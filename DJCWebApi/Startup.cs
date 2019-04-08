namespace DJCWebApi
{
    using DJCWebApi.Providers;
    using DJCWebApiBO;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;
    using Owin;
    using PI.Core;
    using PI.Core.BL;
    using PI.Core.DA;
    using PI.Core.Tools;
    using pi.ds;
    using PI.ws;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class Startup
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static OAuthAuthorizationServerOptions <OAuthOptions>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static string <PublicClientId>k__BackingField;
        private static string CONFIGFILEPATH = (AppDomain.CurrentDomain.BaseDirectory + @"\Setting.xml");

        [AsyncStateMachine(typeof(<BackgroudTask>d__14)), DebuggerStepThrough]
        private Task BackgroudTask()
        {
            <BackgroudTask>d__14 stateMachine = new <BackgroudTask>d__14 {
                <>4__this = this,
                <>t__builder = AsyncTaskMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<BackgroudTask>d__14>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        private void ConfigDataSource()
        {
            AppSettingVO gvo = XmlSerializeUtil.Deserialize<AppSettingVO>(CONFIGFILEPATH);
            DataSource ds = new DataSource {
                DB = gvo.DB,
                Server = gvo.Server,
                Userid = gvo.Userid,
                Pwd = gvo.Pwd
            };
            Setting.AddDataSource(ds, DataSourceKind.Logger | DataSourceKind.Data);
            Encryptor.Encrypt(gvo.MESPwd);
            if (gvo.MESServer > null)
            {
                DataSource source2 = new DataSource {
                    DB = gvo.MESDB,
                    Server = gvo.MESServer,
                    Userid = gvo.MESUserid,
                    Pwd = gvo.MESPwd,
                    Name = "mesdb"
                };
                Setting.AddDataSource(source2, DataSourceKind.Data);
            }
            WSPublisherManager.PublisherManager.URL = gvo.WSUrl;
            PI.Core.Core.Init();
            PI.Core.Core.ServerNo = gvo.ServerNo;
            PI.Core.Core.PITaskLogPrint = true;
            this.StartBackgroudTask();
        }

        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            this.ConfigDataSource();
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationDbContext>(new Func<ApplicationDbContext>(ApplicationDbContext.Create));
            app.CreatePerOwinContext<ApplicationUserManager>(new Func<IdentityFactoryOptions<ApplicationUserManager>, IOwinContext, ApplicationUserManager>(ApplicationUserManager.Create));
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie("ExternalCookie");
            PublicClientId = "self";
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions {
                TokenEndpointPath = new PathString("/api/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(14.0),
                RefreshTokenProvider = new OpenRefreshTokenProvider(),
                AllowInsecureHttp = true
            };
            OAuthOptions = options;
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        public void ConfigureAuth2(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions {
                AllowInsecureHttp = true,
                AuthenticationMode = AuthenticationMode.Active,
                TokenEndpointPath = new PathString("/token"),
                AuthorizeEndpointPath = new PathString("/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(10.0),
                Provider = new OpenAuthorizationServerProvider(),
                AuthorizationCodeProvider = new OpenAuthorizationCodeProvider(),
                RefreshTokenProvider = new OpenRefreshTokenProvider()
            };
            app.UseOAuthBearerTokens(options);
        }

        [AsyncStateMachine(typeof(<StartBackgroudTask>d__13)), DebuggerStepThrough]
        private void StartBackgroudTask()
        {
            <StartBackgroudTask>d__13 stateMachine = new <StartBackgroudTask>d__13 {
                <>4__this = this,
                <>t__builder = AsyncVoidMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<StartBackgroudTask>d__13>(ref stateMachine);
        }

        public static OAuthAuthorizationServerOptions OAuthOptions
        {
            [CompilerGenerated]
            get => 
                <OAuthOptions>k__BackingField;
            [CompilerGenerated]
            private set => 
                (<OAuthOptions>k__BackingField = value);
        }

        public static string PublicClientId
        {
            [CompilerGenerated]
            get => 
                <PublicClientId>k__BackingField;
            [CompilerGenerated]
            private set => 
                (<PublicClientId>k__BackingField = value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Startup.<>c <>9 = new Startup.<>c();
            public static Action<Task> <>9__14_0;

            internal void <BackgroudTask>b__14_0(Task t)
            {
                try
                {
                    DataServiceManager.Manager.AutoInstallTask();
                }
                catch (Exception exception)
                {
                    Logger.Error(exception);
                }
            }
        }

        [CompilerGenerated]
        private sealed class <BackgroudTask>d__14 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public Startup <>4__this;
            private IPAddressManager <im>5__1;
            private Task <tsk>5__2;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        this.<im>5__1 = IPAddressManager.Manager;
                        PITaskLogger.ClearAll();
                        this.<tsk>5__2 = Task.Delay(0x2710);
                        awaiter = this.<tsk>5__2.ContinueWith(Startup.<>c.<>9__14_0 ?? (Startup.<>c.<>9__14_0 = new Action<Task>(Startup.<>c.<>9.<BackgroudTask>b__14_0))).GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            Startup.<BackgroudTask>d__14 stateMachine = this;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Startup.<BackgroudTask>d__14>(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
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
        private sealed class <StartBackgroudTask>d__13 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public Startup <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        Logger.Info("start pitask");
                        awaiter = this.<>4__this.BackgroudTask().GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            Startup.<StartBackgroudTask>d__13 stateMachine = this;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, Startup.<StartBackgroudTask>d__13>(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter();
                        this.<>1__state = num = -1;
                    }
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
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

        public class AppSettingVO
        {
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <DB>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <Server>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <Userid>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <Pwd>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <WSUrl>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private int <ServerNo>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <MESDB>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <MESServer>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <MESUserid>k__BackingField;
            [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string <MESPwd>k__BackingField;

            public string DB { get; set; }

            public string Server { get; set; }

            public string Userid { get; set; }

            public string Pwd { get; set; }

            public string WSUrl { get; set; }

            public int ServerNo { get; set; }

            public string MESDB { get; set; }

            public string MESServer { get; set; }

            public string MESUserid { get; set; }

            public string MESPwd { get; set; }
        }
    }
}

