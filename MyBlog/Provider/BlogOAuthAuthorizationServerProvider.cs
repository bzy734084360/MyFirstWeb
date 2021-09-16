using BlogBusinessLogic;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
         * 获取Token时都会验证
         * 实现对Client的验证
         */
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }
            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "clientId 不能为空");
                return Task.FromResult<object>(null);
            }
            if (!string.IsNullOrEmpty(clientSecret))
            {
                context.OwinContext.Set("clientSecret", clientSecret);
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
        /*
         * 认证Client后进行对应跳转 授权码模式*
         */
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var client = new BlogClientManager().QueryEntity(context.ClientId);
            if (client != null)
            {
                context.Validated(client.RedirectUrl);
            }
            return base.ValidateClientRedirectUri(context);
        }

        /*
         * 实现基于用户密码模式获取Access Token
         */
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);
            if (userManager != null)
            {
                var user = userManager.FindAsync(context.UserName, context.Password).Result;
                if (user == null)
                {
                    context.SetError("invalid_grant", "用户名或密码不存在");
                    return Task.FromResult<object>(null);
                }
                var identity = new ClaimsIdentity(
                    new GenericIdentity(context.UserName, OAuthDefaults.AuthenticationType),
                    context.Scope.Select(t => new Claim("urn:oauth:scope", t)));
                context.Validated(identity);
            }
            return Task.FromResult(0);
        }

        /*
         * 客户端认证模式
         */
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var secret = context.OwinContext.Get<string>("clientSecret");
            var client = new BlogClientManager().QueryEntity(context.ClientId);
            if (client != null && client.Secret == secret)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(context.ClientId, OAuthDefaults.AuthenticationType),
                    context.Scope.Select(t => new Claim("urn:oauth:scope", t)));
                context.Validated(identity);
            }
            return Task.FromResult(0);
        }


    }
}