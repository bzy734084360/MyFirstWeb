using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Utilities
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public partial class SystemInfo
    {
        /// <summary>
        /// 服务实现
        /// </summary>
        public static string Service = "Bzy.ServiceCaller";

        /// <summary>
        /// 服务映射工厂
        /// </summary>
        public static string ServiceFactory = "ServiceFactory";
        /// <summary>
        /// 阿里云AccessKeyID
        /// </summary>
        public static string AliyunAccessKeyID = string.Empty;
        /// <summary>
        /// 阿里云AccessKeySecret
        /// </summary>
        public static string AliyunAccessKeySecret = string.Empty;
    }
}
