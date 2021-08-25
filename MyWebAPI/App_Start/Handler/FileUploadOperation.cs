using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace MyWebAPI.App_Start.Handler
{
    /// <summary>
    /// 配置文件上传过滤
    /// </summary>
    public class FileUploadOperation : IOperationFilter
    {
        /// <summary>
        /// 实现
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            //ControllerName_ActionName
            //operation.operationId
            if ("ControllerName_ActionName" == operation.operationId)
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>();
                operation.parameters.Add(new Parameter
                {
                    name = "file",
                    @in = "formData",
                    description = "Upload File",
                    required = true,
                    type = "file"
                });
                operation.consumes.Add("multipart/form-data");
            }
        }
    }
}