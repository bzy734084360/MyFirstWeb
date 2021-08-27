using Bzy.ServiceCaller;
using Bzy.Utilities;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyWeb.Provider
{
    public class BzyOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /*
         * 实现基于授权码模式的身份验证  ( Authorization Code)
         * 授权服务器对客户端验证
         */
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //尝试从header或请求的body中获取请求的client 信息 包含Id和密码
            string clientId, clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }
            //账号Id为空则抛出异常
            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
            }
            //密码信息不为空则保存在Owin的上下文中
            if (!string.IsNullOrEmpty(clientSecret))
            {
                context.OwinContext.Set("clientSecret", clientSecret);
            }

            //校验查询client信息
            var client = BzyService.Instance.BzyUserService.QueryEntityByRegister(clientId);
            if (client != null)
            {
                context.Validated();
            }
            else
            {
                context.SetError("invalid_clientid", $"Invalid client_id '{context.ClientId}'");
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 设置重定向Url
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var client = BzyService.Instance.BzyUserService.QueryEntityByRegister(context.ClientId);//通过clientid 查询client信息
            if (client != null)
            {
                //从client对象中获取到Url 进行赋值
                context.Validated("http://localhost:53448/");
            }
            return base.ValidateClientRedirectUri(context);
        }

    }
}