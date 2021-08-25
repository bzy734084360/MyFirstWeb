using Bzy.BizLogic;
using Bzy.ServiceCaller;
using Bzy.Utilities;
using MyWebAPI.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MyWebAPI.Areas.GrumpyFish.Controllers
{
    /// <summary>
    /// 用户相关服务
    /// </summary>
    [RoutePrefix("api/User")]
    public class UserController : ApiControllerBase
    {
        /// <summary>
        /// 获取token示例
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetLoginToken()
        {
            //请求登录获取Token
            string url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority + "/token";
            HttpHelper httpHelper = new HttpHelper();
            IDictionary<string, object> param = new Dictionary<string, object>()
            {
            {"grant_type", "password" },
            {"username", CurrentUser.UserName},
            {"password", string.Empty},
            };

            StringBuilder postString = new StringBuilder();
            bool first_param = true;
            if (param != null)
            {
                foreach (var p in param)
                {
                    if (first_param)
                        first_param = false;
                    else
                        postString.Append("&");
                    postString.AppendFormat("{0}={1}", p.Key, p.Value);
                }
            }
            string token;
            HttpItem item = new HttpItem
            {
                URL = url,
                Method = "POST",
                Postdata = postString.ToString(),
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",
            };
            HttpResult httpresult = httpHelper.GetHtml(item);
            if (httpresult.StatusCode == HttpStatusCode.OK)
            {
                JObject jObject = JObject.Parse(httpresult.Html);
                if (jObject["isLogin"] != null && "0".Equals(jObject["isLogin"].ToString()) &&
                    jObject["access_token"] != null && !string.IsNullOrEmpty(jObject["access_token"].ToString()))
                {
                    token = jObject["access_token"].ToString();
                }
                else
                {
                    throw new ApiCustomException("解析token异常", 100);
                }
            }
            else
            {
                throw new ApiCustomException("解析token异常", 100);
            }
            return ToResMsgJson("获取成功", new { access_token = "Bearer " + token });
        }
    }
}
