namespace DJCWebApi.Controllers
{
    using DJCWebApi.Utils;
    using DJCWebApi.ws.KB.DataCollecters;
    using Newtonsoft.Json;
    using PI.Core.DA;
    using PI.Core.vo;
    using PI.ws;
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/kb")]
    public class KBController : ApiController
    {
        [AsyncStateMachine(typeof(<Async1>d__7)), DebuggerStepThrough]
        private void Async1()
        {
            <Async1>d__7 stateMachine = new <Async1>d__7 {
                <>4__this = this,
                <>t__builder = AsyncVoidMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<Async1>d__7>(ref stateMachine);
        }

        [AsyncStateMachine(typeof(<Async2>d__8)), DebuggerStepThrough]
        private void Async2()
        {
            <Async2>d__8 stateMachine = new <Async2>d__8 {
                <>4__this = this,
                <>t__builder = AsyncVoidMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<Async2>d__8>(ref stateMachine);
        }

        [HttpGet, Route("at")]
        public string AsynTest()
        {
            Debug.WriteLine("AsynTest begin");
            this.Async1();
            this.Async2();
            Debug.WriteLine("AsynTest end");
            return "";
        }

        private KBPublisher createKBpublisher(string cj)
        {
            KBPublisher puber = new KBPublisher {
                Identity = "kbservice" + cj.ToLower(),
                Workflow = cj.ToUpper()
            };
            WSPublisherManager.PublisherManager.AddPublisher(puber);
            puber.Start();
            return puber;
        }

        public HttpResponseMessage Get()
        {
            KBPublisher publisher;
            if (!WSPublisherManager.PublisherManager.Contain("kbdataservice"))
            {
                this.createKBpublisher("hd");
                publisher = this.createKBpublisher("zxsxa");
                this.createKBpublisher("zxsxb");
                this.createKBpublisher("zxjxa");
                this.createKBpublisher("zxjxb");
                this.createKBpublisher("zxjxc");
                this.createKBpublisher("zxjxd");
                this.createKBpublisher("zxtr");
                this.createKBpublisher("zxtxa");
                this.createKBpublisher("zxtxb");
                this.createKBpublisher("zxdzl");
                this.createKBpublisher("zxzt");
            }
            else
            {
                publisher = (KBPublisher) WSPublisherManager.PublisherManager.Publisher("zxsxa");
            }
            return HttpHelper.toJson(publisher.Datas);
        }

        public HttpResponseMessage Get(int id)
        {
            string[] strArray = new string[] { "hd", "zxsxa", "zxsxb", "zxjxa", "zxjxb", "zxjxc", "zxjxd", "zxtr", "zxtxa", "zxtxb", "zxdzl", "zxzt" };
            KBPublisher publisher = (KBPublisher) WSPublisherManager.PublisherManager.Publisher("kbservice" + strArray[id].ToLower());
            if (publisher == null)
            {
                return new HttpResponseMessage { Content = new StringContent("no service[" + strArray[id] + "]", Encoding.UTF8, "application/json") };
            }
            return HttpHelper.toJson(publisher.Datas);
        }

        private async Task<string> getData()
        {
            await Task.Delay(0xbb8);
            Debug.WriteLine("running done");
            return await Task.FromResult<string>("running done");
        }

        private string getMsg() => 
            "{\"Plannum\":14264,\"Actualnum\":1378,\"Completerate\":9.660000,\"Mocount\":98,\"Mooutcount\":28,\"Passrate\":88,\"Machinenum\":128,\"Machopennum\":108,\"Machmannum\":9,\"Weighmannum\":1,\"Machhourmonth\":60016,\"Houroutlist\":{\"cols\":[\"1:00\",\"2:00\",\"3:00\",\"4:00\",\"5:00\",\"6:00\",\"7:00\",\"8:00\",\"9:00\",\"10:00\",\"11:00\"],\"vals\":[1577.46000000,1935.90000000,606.70000000,2316.20000000,2756.02000000,189.70000000,1266.80000000,179.40000000,0]},\"dayhourlist\":{\"cols\":[\"1\",\"2\",\"3\",\"4\",\"5\",\"6\",\"7\",\"8\",\"9\",\"10\",\"11\",\"12\",\"13\",\"14\",\"15\"],\"mhour\":[4367.00000000,2239.90000000,3117.00000000,4321.20000000,4414.02000000,4387.00000000,4376.00000000,4251.00000000,4296.000000,4572.00000000,4373.00000000,4427.00000000,4401.00000000,4356.00000000,2119.50000000],\"shour\":[6864,2292,3288,4584,4584,4584,4596,4584,4596,4572,4584,4584,4584,4584,2292]},\"empoutputlist\":{\"cols\":[\"张三\",\"李四\",\"王五\",\"钱六\",\"赵七\",\"孙八\",\"陈九\",\"陆十\",\"贾二\",\"唐一\"],\"todays\":[53,78,87,89,112,115,123,132,140,145],\"yestodays\":[120,98,115,83,69,142,105,93,121,87]},\"detaillist\":[{\"key\":1,\"custno\":\"B0BS0140\",\"empno\":\"862\",\"kjtime\":12.00000000,\"leftweigh\":4650.60000000,\"pointno\":\"TYX248\",\"mcode\":\"TYJ0000059A1B0320000000\",\"morderno\":\"17085749\",\"protype\":14,\"sjchanneng\":27.98700000,\"sprc\":\"320TY0.059\",\"speed\":1600,\"weighnum\":0.00000000},{\"key\":2,\"custno\":\"B0JD0306\",\"empno\":\"932\",\"kjtime\":12.00000000,\"leftweigh\":170.54000000,\"pointno\":\"TYX197\",\"mcode\":\"TYJ0000083A1B0320000000\",\"morderno\":\"17092805\",\"protype\":14,\"sjchanneng\":65.77300000,\"sprc\":\"0.083\",\"speed\":1900,\"weighnum\":43.90000000},{\"key\":3,\"custno\":\"B0BS0140\",\"empno\":\"038\",\"kjtime\":12.00000000,\"leftweigh\":3909989.67000000,\"pointno\":\"TYX223\",\"mcode\":\"TYJ0000059A1B0320000000\",\"morderno\":\"17080289\",\"protype\":14,\"sjchanneng\":27.98700000,\"sprc\":\"320TY0.059\",\"speed\":1600,\"weighnum\":0.00000000},{\"key\":4,\"custno\":\"B0BS0140\",\"empno\":\"932\",\"kjtime\":12.00000000,\"leftweigh\":3909989.67000000,\"pointno\":\"TYX171\",\"mcode\":\"TYJ0000059A1B0320000000\",\"morderno\":\"17080289\",\"protype\":14,\"sjchanneng\":27.98700000,\"sprc\":\"320TY0.059\",\"speed\":1600,\"weighnum\":17.50000000},{\"key\":5,\"custno\":\"A0FQ0026\",\"empno\":\"3562\",\"kjtime\":12.00000000,\"leftweigh\":2898.83000000,\"pointno\":\"TYX272\",\"mcode\":\"TYJ0000055A1B0320000000\",\"morderno\":\"17085296\",\"protype\":14,\"sjchanneng\":21.28100000,\"sprc\":\"0.055\",\"speed\":1400,\"weighnum\":0.00000000}]}";

        [AsyncStateMachine(typeof(<Msg>d__2)), DebuggerStepThrough]
        private void Msg(KBPublisher kk)
        {
            <Msg>d__2 stateMachine = new <Msg>d__2 {
                <>4__this = this,
                kk = kk,
                <>t__builder = AsyncVoidMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<Msg>d__2>(ref stateMachine);
        }

        public HttpResponseMessage Post([FromBody] UserVO value) => 
            this.ReturnJson(0);

        [HttpGet, Route("test")]
        public HttpResponseMessage ReturnJson(int id)
        {
            UserVO rvo = new UserVO {
                Name = "alun",
                Code = "GZ",
                Level = id
            };
            string content = JsonConvert.SerializeObject(rvo);
            return new HttpResponseMessage { Content = new StringContent(content, Encoding.UTF8, "application/json") };
        }

        [CompilerGenerated]
        private sealed class <Async1>d__7 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public KBController <>4__this;
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<string> awaiter;
                    if (num != 0)
                    {
                        Debug.WriteLine("async1 begin");
                        awaiter = this.<>4__this.getData().GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            KBController.<Async1>d__7 stateMachine = this;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, KBController.<Async1>d__7>(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                    }
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter<string>();
                    Debug.WriteLine("async1 done");
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
        private sealed class <Async2>d__8 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public KBController <>4__this;
            private string <rst>5__1;
            private string <>s__2;
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<string> awaiter;
                    if (num != 0)
                    {
                        Debug.WriteLine("async2 begin");
                        awaiter = this.<>4__this.getData().GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            KBController.<Async2>d__8 stateMachine = this;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, KBController.<Async2>d__8>(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                    }
                    string result = awaiter.GetResult();
                    awaiter = new TaskAwaiter<string>();
                    this.<>s__2 = result;
                    this.<rst>5__1 = this.<>s__2;
                    this.<>s__2 = null;
                    Debug.WriteLine(this.<rst>5__1 + " async2 done");
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
        private sealed class <Msg>d__2 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public KBPublisher kk;
            public KBController <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        awaiter = Task.Delay(0xbb8).GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            KBController.<Msg>d__2 stateMachine = this;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, KBController.<Msg>d__2>(ref awaiter, ref stateMachine);
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
                    this.kk.Datas = new DBData();
                    this.kk.Datas.Add("msg", this.<>4__this.getMsg());
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

