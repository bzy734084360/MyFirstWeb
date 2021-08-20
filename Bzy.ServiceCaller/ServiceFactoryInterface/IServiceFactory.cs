using Bzy.BizLogic.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.ServiceCaller
{
    /// <summary>
    /// 服务工厂接口定义
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// 创建Auth刷新Token表服务
        /// </summary>
        /// <returns>服务接口</returns>
        IRefreshTokenService CreateRefreshTokenService();
    }
}
