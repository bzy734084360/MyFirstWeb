using BlogBusinessLogic;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyBlog.Provider
{
    /// <summary>
    /// OAuth服务
    /// </summary>
    public class BlogOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /*
         * 生成授权码后对授权码进行校验
         * 实现对Client的验证
         */
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSercet;
            if (!context.TryGetBasicCredentials(out clientId, out clientSercet))
            {
                context.TryGetFormCredentials(out clientId, out clientSercet);
            }
            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "clientId 不能为空");
                return Task.FromResult<object>(null);
            }
            if (!string.IsNullOrEmpty(clientSercet))
            {
                context.OwinContext.Set("clientSercet", clientSercet);
            }
            var client = new BlogClientManager().QueryEntity(clientId);
            if (client != null)
            {
                //if (clientSercet == client.Sercet)
                //{
                context.Validated();
                //}
                //else
                //{
                //    context.SetError("invalic_clientSercet", $"无效的 clientSercet:{clientSercet}");
                //    return Task.FromResult<object>(null);
                //}

            }
            else
            {
                context.SetError("invalic_clientId", $"无效的 client_Id:{clientId}");
                return Task.FromResult<object>(null);
            }

            return Task.FromResult<object>(null);
        }
        /// <summary>
        /// 认证Client后进行对应跳转
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var client = new BlogClientManager().QueryEntity(context.ClientId);
            if (client != null)
            {
                context.Validated(client.RedirectUrl);
            }
            return base.ValidateClientRedirectUri(context);
        }
    }
}