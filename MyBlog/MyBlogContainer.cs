using Autofac;
using BlogBusinessLogic;
using MyBlog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog
{
    /// <summary>
    /// 依赖注入容器 
    /// </summary>
    public static class MyBlogContainer
    {
        /// <summary>
        /// 获取容器
        /// </summary>
        /// <returns></returns>
        public static IContainer GetContainer()
        {
            //创建容器构筑对象
            var builder = new ContainerBuilder();
            //添加容器包含的"水"类型？？？
            builder.RegisterType<BlogManager>();
            builder.RegisterType<PostController>();
            var container = builder.Build();
            return container;
        }
    }
}