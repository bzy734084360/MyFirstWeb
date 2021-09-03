using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog
{
    /// <summary>
    /// 依赖解析器
    /// </summary>
    public class MyBlogDependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope _container;

        public MyBlogDependencyResolver(ILifetimeScope container)
        {
            _container = container;
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            try
            {
                //从容器中解析类型获取对应的实例
                var instance = _container.Resolve(serviceType);
                return instance;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取服务集合
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                //解析类型转换成IEnumberable集合对象
                var enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
                var instance = _container.Resolve(enumerableServiceType);
                return (IEnumerable<object>)instance;
            }
            catch
            {
                return null;
            }
        }
    }
}