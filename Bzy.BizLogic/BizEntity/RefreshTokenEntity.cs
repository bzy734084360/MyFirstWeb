using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.BizLogic.BizEntity
{
    /// <summary>
    /// 刷新Token
    /// </summary>
    public class RefreshTokenEntity
    {
        /// <summary>
        /// 刷新Token值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime IssuedUtc { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime ExpiresUtc { get; set; }
        /// <summary>
        /// 受保护的票据标识
        /// </summary>
        public string ProtectedTicket { get; set; }
    }
}
