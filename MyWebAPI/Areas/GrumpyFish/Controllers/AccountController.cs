using Bzy.BizLogic;
using Bzy.ServiceCaller;
using Bzy.Utilities;
using MyWebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebAPI.Areas.GrumpyFish.Controllers
{
    /// <summary>
    /// 账户相关
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Register(RegisterRequest model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.UserPassword))
                throw new ApiCustomException("参数异常");

            var userEntity = BzyService.Instance.BzyUserService.QueryEntityByRegister(model.UserName);
            if (userEntity != null)
                throw new ApiCustomException("账号已存在");

            BzyUserEntity bzyUserEntity = new BzyUserEntity
            {
                UserName = model.UserName,
                UserPassword = SecretHelper.AESEncrypt(model.UserPassword)
            };
            if (BzyService.Instance.BzyUserService.AddEntity(bzyUserEntity) != 1)
                throw new ApiCustomException("注册失败");


            return ToResMsgJson("注册成功");
        }
    }
}
