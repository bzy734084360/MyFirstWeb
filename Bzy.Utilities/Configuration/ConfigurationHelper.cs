using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Utilities
{
    public class ConfigurationHelper
    {
        public static void GetConfig()
        {
            //阿里云
            if (ConfigurationManager.AppSettings["AliyunAccessKeyID"] != null)
                SystemInfo.AliyunAccessKeyID = ConfigurationManager.AppSettings["AliyunAccessKeyID"];
        }
    }
}
