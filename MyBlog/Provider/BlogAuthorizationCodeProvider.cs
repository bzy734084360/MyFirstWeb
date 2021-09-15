using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyBlog.Provider
{
    /*
     * 令牌提供器 IAuthenticationTokenProvider
     * 添加授权码提供器 (授权码的生成是授权服务器终结点的一项功能) Startup 配置的AuthorizeEndpointPath 值
     */
    public class BlogAuthorizationCodeProvider : IAuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> _authorizationCodes =
            new ConcurrentDictionary<string, string>(StringComparer.Ordinal);
        /*
         * 创建授权码
         */
        public void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _authorizationCodes[context.Token] = context.SerializeTicket();
        }
        /*
         * 移除授权码
         */
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_authorizationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }
        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return Task.Run(() =>
            {
                this.Create(context);
            });
        }
        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return Task.Run(() =>
            {
                this.Receive(context);
            });
        }
    }
}