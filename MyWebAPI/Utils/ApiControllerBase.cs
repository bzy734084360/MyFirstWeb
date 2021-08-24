using Bzy.Utilities;
using MyWebAPI.App_Start.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace MyWebAPI.Utils
{
    /// <summary>
    /// API接口父类
    /// </summary>
    [BzyAuthorizeFilter, WebApiExceptionFilter]
    public class ApiControllerBase : ApiController
    {
        /// <summary>
        /// 当前Token用户信息
        /// </summary>
        public UserData CurrentUser
        {
            get
            {
                try
                {
                    var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
                    var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.UserData);
                    UserData user = claim.Value.JsonToEntity<UserData>();
                    if (user == null)
                    {
                        throw new Exception("登录信息超时，请重新登录。");
                    }
                    return user;
                }
                catch
                {
                    throw new Exception("登录信息超时，请重新登录。");
                }
            }
        }
        /// <summary>
        /// 定义接口统一出参
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage ToResMsgJson(object obj = null)
        {
            var result = new { code = 200, data = obj };
            var resultJson = result.ToApiJson();

            return new HttpResponseMessage
            {
                Content = new StringContent(resultJson, Encoding.GetEncoding("UTF-8"), "application/json")
            };
        }

    }
}