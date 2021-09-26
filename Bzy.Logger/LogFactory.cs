using NLog;
using System;
using System.IO;
using System.Web;

namespace Bzy.Logger
{
    /// <summary>
    /// 日志初始化
    /// </summary>
    public class LogFactory
    {

        public static Log GetLogger( )
        {
            return new Log(LogManager.GetCurrentClassLogger());
        }
        public static Log GetLogger(Type type)
        {
            return new Log(LogManager.GetCurrentClassLogger(type));
        }
    }
}
