namespace DJCWebApi.ws.KB.DataCollecters
{
    using PI.Core.DA;
    using PI.ws;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class DemoPublisher : Publisher
    {
        private DBData dates;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DBData <OldDatas>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Identity>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string[] <Receiver>k__BackingField;

        [field: CompilerGenerated, DebuggerBrowsable(0)]
        public event CollecterDataChangeHandler DataChanged;

        [AsyncStateMachine(typeof(<ResetDatas>d__1)), DebuggerStepThrough]
        private Task ResetDatas()
        {
            <ResetDatas>d__1 stateMachine = new <ResetDatas>d__1 {
                <>4__this = this,
                <>t__builder = AsyncTaskMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<ResetDatas>d__1>(ref stateMachine);
            return stateMachine.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<Start>d__0)), DebuggerStepThrough]
        public void Start()
        {
            <Start>d__0 stateMachine = new <Start>d__0 {
                <>4__this = this,
                <>t__builder = AsyncVoidMethodBuilder.Create(),
                <>1__state = -1
            };
            stateMachine.<>t__builder.Start<<Start>d__0>(ref stateMachine);
        }

        public DBData OldDatas { get; protected set; }

        public DBData Datas
        {
            get => 
                this.dates;
            set
            {
                this.OldDatas = this.dates;
                this.dates = value;
                this.DataChanged(this);
            }
        }

        public string Identity { get; set; }

        public string[] Receiver { get; set; }

        [CompilerGenerated]
        private sealed class <ResetDatas>d__1 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public DemoPublisher <>4__this;
            private Random <ran>5__1;
            private int <RandKey>5__2;
            private string <msg>5__3;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    bool flag;
                    if (num == 0)
                    {
                        goto Label_00C5;
                    }
                    goto Label_0100;
                Label_0017:
                    this.<ran>5__1 = new Random();
                    this.<RandKey>5__2 = this.<ran>5__1.Next(100, 0x3e7);
                    this.<msg>5__3 = "test message " + this.<RandKey>5__2;
                    Debug.WriteLine("puber push message: " + this.<msg>5__3);
                    this.<>4__this.Datas.Add("msg", this.<msg>5__3);
                    TaskAwaiter awaiter = Task.Delay(0x2710).GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        goto Label_00E1;
                    }
                    this.<>1__state = num = 0;
                    this.<>u__1 = awaiter;
                    DemoPublisher.<ResetDatas>d__1 stateMachine = this;
                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, DemoPublisher.<ResetDatas>d__1>(ref awaiter, ref stateMachine);
                    return;
                Label_00C5:
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter();
                    this.<>1__state = num = -1;
                Label_00E1:
                    awaiter.GetResult();
                    awaiter = new TaskAwaiter();
                    this.<ran>5__1 = null;
                    this.<msg>5__3 = null;
                Label_0100:
                    flag = true;
                    goto Label_0017;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        [CompilerGenerated]
        private sealed class <Start>d__0 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncVoidMethodBuilder <>t__builder;
            public DemoPublisher <>4__this;
            private TaskAwaiter <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter awaiter;
                    if (num != 0)
                    {
                        awaiter = this.<>4__this.ResetDatas().GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            DemoPublisher.<Start>d__0 stateMachine = this;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, DemoPublisher.<Start>d__0>(ref awaiter, ref stateMachine);
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
    }
}

