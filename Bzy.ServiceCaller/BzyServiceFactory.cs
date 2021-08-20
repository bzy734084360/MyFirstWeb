using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.ServiceCaller
{
    public abstract class BzyServiceFactory
    {
        /// <summary>
        /// 映射接口
        /// </summary>
        /// <param name="servicePath"></param>
        /// <param name="serviceFactoryClass"></param>
        /// <returns></returns>
        public IServiceFactory GetServiceFactory(string servicePath, string serviceFactoryClass)
        {
            string className = servicePath + "." + serviceFactoryClass;
            return (IServiceFactory)Assembly.Load(servicePath).CreateInstance(className);
        }
    }
}
