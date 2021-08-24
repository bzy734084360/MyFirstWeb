using Bzy.BizLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.ServiceCaller
{
    /// <summary>
    /// 本地服务的具体实现接口
    /// </summary>
    public class ServiceFactory : IServiceFactory
    {
        /// <summary>
        /// 创建Auth刷新Token表服务
        /// </summary>
        /// <returns>服务接口</returns>
        public IRefreshTokenService CreateRefreshTokenService()
        {
            return new RefreshTokenService();
        }
        /// <summary>
        /// 创建用户表服务
        /// </summary>
        /// <returns>服务接口</returns>
        public IBzyUserService CreateBzyUserService()
        {
            return new BzyUserService();
        }
    }
}
