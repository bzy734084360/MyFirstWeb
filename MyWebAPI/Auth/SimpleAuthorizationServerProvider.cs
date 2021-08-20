﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MyWebAPI.Auth
{
    /// <summary>
    /// 令牌认证（密码模式（resource owner password credentials））
    /// </summary>
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /*
         * ValidateClientAuthentication方法用来对third party application 认证，
         * 具体的做法是为third party application颁发appKey和appSecrect，
         * 在本例中我们省略了颁发appKey和appSecrect的环节，我们认为所有的third party application都是合法的，
         * context.Validated(); 表示所有允许此third party application请求。*/
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        /*
         * GrantResourceOwnerCredentials方法则是resource owner password credentials模式的重点，
         * 由于客户端发送了用户的用户名和密码，所以我们在这里验证用户名和密码是否正确，
         * 后面的代码采用了ClaimsIdentity认证方式，其实我们可以把他当作一个NameValueCollection看待。
         * 最后context.Validated(ticket); 表明认证通过。
         */
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            /* 密码模式 resource owner password credentials模式需要body包含3个参数：
             * grant_type - 必须为password
             * username - 用户名
             * password - 用户密码*/
            //无数据库链接 不做验证
            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    IdentityUser user = await _repo.FindUser(context.UserName, context.Password);
            //    if (user == null)
            //    {
            if (string.IsNullOrEmpty(context.Request.ReadFormAsync().Result.Get("version")))
            {
                context.SetError("invalid_grant", "用户名或密码无效");
                return;
            }
            //    }
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.UserData, userInfo.ToJson()));//ClaimTypes.UserData 可进行自动用户数据管理
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim("sub", context.UserName));
            var props = new AuthenticationProperties(
                new Dictionary<string, string> {
                    {
                        "as:client_id",context.ClientId??string.Empty
                    },
                    {
                        "userName",context.UserName
                    }
                });
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            return;
            await base.GrantResourceOwnerCredentials(context);
        }
        /*
         * TokenEndpoint方法将会把Context中的属性加入到token中。
         */
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="isState">状态</param>
        /// <param name="message">提示信息</param>
        /// <param name="IsLogin">是否通过登录验证 0 正常 1 失败</param>
        public static AuthenticationProperties CreateProperties(string userName, string isState, string message, string IsLogin)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName },{ "isState",isState},{ "message",message},{ "isLogin",IsLogin}
            };
            return new AuthenticationProperties(data);
        }

    }
}