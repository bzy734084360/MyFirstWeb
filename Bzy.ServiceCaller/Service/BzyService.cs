using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.ServiceCaller.Service
{
    public class BzyService : BzyServiceFactory
    {
        private static BzyService instance = null;
        private static object locker = new object();
        private static readonly string ServicePath = SystemInfo.Service;
        private static readonly string ServiceFactoryClass = SystemInfo.ServiceFactory;
        private IServiceFactory serviceFactory = null;

        public BzyService()
        {
            serviceFactory = GetServiceFactory(ServicePath, ServiceFactoryClass);
        }
        /// <summary>
        /// 创建实例
        /// </summary>
        public static BzyService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new BzyService();
                        }
                    }
                }
                return instance;
            }
        }

        ///// <summary>
        ///// 登录服务
        ///// </summary>
        //public ILogOnService LogOnService => serviceFactory.CreateLogOnService();
    }
}
