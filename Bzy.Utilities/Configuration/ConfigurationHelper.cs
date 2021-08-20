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
            if (ConfigurationManager.AppSettings["BzyDbConnection"] != null)
                SystemInfo.BzyDbConnection = ConfigurationManager.AppSettings["BzyDbConnection"];
        }
    }
}
