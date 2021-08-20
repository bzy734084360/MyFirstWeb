using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Utilities
{
    /// <summary>
    /// 连接配置
    /// </summary>
    public class BaseConfiguration
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseConfiguration()
        {
        }

        public static void GetSetting()
        {
            //读取配置文件
            ConfigurationHelper.GetConfig();
        }
    }
}
