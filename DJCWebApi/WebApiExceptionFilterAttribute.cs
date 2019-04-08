namespace DJCWebApi
{
    using DJCWebApi.Utils;
    using System;
    using System.Diagnostics;
    using System.Web.Http.Filters;

    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" + actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" + actionExecutedContext.Exception.StackTrace);
            Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" + actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" + actionExecutedContext.Exception.StackTrace);
            ExceptionVO nvo = new ExceptionVO {
                ExceptionMessage = actionExecutedContext.Exception.Message
            };
            actionExecutedContext.Response = HttpHelper.toJson(nvo);
            base.OnException(actionExecutedContext);
        }
    }
}

