namespace DJCWebApi.Results
{
    using Microsoft.CSharp.RuntimeBinder;
    using System;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Runtime.CompilerServices;

    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                object obj2 = request.Properties["MS_HttpContext"];
                if (<>o__3.<>p__1 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                    <>o__3.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                }
                if (<>o__3.<>p__0 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null) };
                    <>o__3.<>p__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                }
                if (<>o__3.<>p__1.Target(<>o__3.<>p__1, <>o__3.<>p__0.Target(<>o__3.<>p__0, obj2, null)))
                {
                    if (<>o__3.<>p__4 == null)
                    {
                        <>o__3.<>p__4 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(DJCWebApi.Results.HttpRequestMessageExtensions)));
                    }
                    if (<>o__3.<>p__3 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__3.<>p__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "UserHostAddress", typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                    }
                    if (<>o__3.<>p__2 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__3.<>p__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Request", typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                    }
                    return <>o__3.<>p__4.Target(<>o__3.<>p__4, <>o__3.<>p__3.Target(<>o__3.<>p__3, <>o__3.<>p__2.Target(<>o__3.<>p__2, obj2)));
                }
            }
            if (request.Properties.ContainsKey("System.ServiceModel.Channels.RemoteEndpointMessageProperty"))
            {
                object obj3 = request.Properties["System.ServiceModel.Channels.RemoteEndpointMessageProperty"];
                if (<>o__3.<>p__6 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                    <>o__3.<>p__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                }
                if (<>o__3.<>p__5 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null) };
                    <>o__3.<>p__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                }
                if (<>o__3.<>p__6.Target(<>o__3.<>p__6, <>o__3.<>p__5.Target(<>o__3.<>p__5, obj3, null)))
                {
                    if (<>o__3.<>p__8 == null)
                    {
                        <>o__3.<>p__8 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(DJCWebApi.Results.HttpRequestMessageExtensions)));
                    }
                    if (<>o__3.<>p__7 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__3.<>p__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Address", typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                    }
                    return <>o__3.<>p__8.Target(<>o__3.<>p__8, <>o__3.<>p__7.Target(<>o__3.<>p__7, obj3));
                }
            }
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                object obj4 = request.Properties["MS_OwinContext"];
                if (<>o__3.<>p__10 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                    <>o__3.<>p__10 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                }
                if (<>o__3.<>p__9 == null)
                {
                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null) };
                    <>o__3.<>p__9 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                }
                if (<>o__3.<>p__10.Target(<>o__3.<>p__10, <>o__3.<>p__9.Target(<>o__3.<>p__9, obj4, null)))
                {
                    if (<>o__3.<>p__13 == null)
                    {
                        <>o__3.<>p__13 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(DJCWebApi.Results.HttpRequestMessageExtensions)));
                    }
                    if (<>o__3.<>p__12 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__3.<>p__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "RemoteIpAddress", typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                    }
                    if (<>o__3.<>p__11 == null)
                    {
                        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
                        <>o__3.<>p__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Request", typeof(DJCWebApi.Results.HttpRequestMessageExtensions), argumentInfo));
                    }
                    return <>o__3.<>p__13.Target(<>o__3.<>p__13, <>o__3.<>p__12.Target(<>o__3.<>p__12, <>o__3.<>p__11.Target(<>o__3.<>p__11, obj4)));
                }
            }
            return null;
        }

        [CompilerGenerated]
        private static class <>o__3
        {
            public static CallSite<Func<CallSite, object, object, object>> <>p__0;
            public static CallSite<Func<CallSite, object, bool>> <>p__1;
            public static CallSite<Func<CallSite, object, object>> <>p__2;
            public static CallSite<Func<CallSite, object, object>> <>p__3;
            public static CallSite<Func<CallSite, object, string>> <>p__4;
            public static CallSite<Func<CallSite, object, object, object>> <>p__5;
            public static CallSite<Func<CallSite, object, bool>> <>p__6;
            public static CallSite<Func<CallSite, object, object>> <>p__7;
            public static CallSite<Func<CallSite, object, string>> <>p__8;
            public static CallSite<Func<CallSite, object, object, object>> <>p__9;
            public static CallSite<Func<CallSite, object, bool>> <>p__10;
            public static CallSite<Func<CallSite, object, object>> <>p__11;
            public static CallSite<Func<CallSite, object, object>> <>p__12;
            public static CallSite<Func<CallSite, object, string>> <>p__13;
        }
    }
}

