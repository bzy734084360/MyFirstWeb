using Bzy.BizLogic;
using Bzy.ServiceCaller;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyWebAPI.Auth
{
    /// <summary>
    /// 刷新token
    /// </summary>
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = Guid.NewGuid().ToString("n");

            var token = new RefreshTokenEntity()
            {
                Id = refreshTokenId,
                UserName = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();
            //添加刷新token记录
            int addResult = await BzyService.Instance.RefreshTokenService.AddEntityAsync(token);

            if (addResult == 1)
            {
                context.SetToken(refreshTokenId);
            }
        }
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            string tokenId = context.Token;
            var refreshToken = await BzyService.Instance.RefreshTokenService.QueryEntityAsync(tokenId);
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await BzyService.Instance.RefreshTokenService.DeleteEntityAsync(tokenId);
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }
    }
}