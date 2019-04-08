namespace DJCWebApi.Controllers
{
    using DJCWebApi;
    using DJCWebApi.Results;
    using DJCWebApi.Utils;
    using DJCWebApiBO.Role;
    using PI.Core.vo.Role;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    [Authorize, WebApiExceptionFilter, RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        [HttpGet, Route("FuncInfo")]
        public HttpResponseMessage GetFuncInfo(string pkfunc)
        {
            string clientIpAddress = base.Request.GetClientIpAddress();
            List<RoleFuncVO> list = new RoleBO().getRoleFuncVOs(base.User.Identity.Name, clientIpAddress);
            bool flag = false;
            foreach (RoleFuncVO cvo2 in list)
            {
                if (cvo2.PKFunc.Equals(pkfunc))
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                throw new Exception("此功能只限内部网络访问!");
            }
            return HttpHelper.toJson(new RoleBO().getFuncVO(pkfunc));
        }

        [HttpGet, Route("UserActionRight")]
        public HttpResponseMessage GetUserActionRight(string func) => 
            HttpHelper.toJson(new RoleBO().getUserRoleVOs(base.User.Identity.Name, func));

        [HttpGet, Route("UserFuncRight")]
        public HttpResponseMessage GetUserFuncRight()
        {
            string clientIpAddress = base.Request.GetClientIpAddress();
            return HttpHelper.toJson(new RoleBO().getRoleFuncVOs(base.User.Identity.Name, clientIpAddress));
        }
    }
}

