using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace MyWebAPI.App_Start.Handler
{
    /// <summary>
    /// Swagger Header信息配置
    /// </summary>
    public class SwaggerHttpHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline(); //判断是否添加权限过滤器
            var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); //判断是否允许匿名方法 
            //获取授权状态
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                || apiDescription.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            //配置请求头添加参数
            if (isAuthorized && !allowAnonymous)
            {
                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "Token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}