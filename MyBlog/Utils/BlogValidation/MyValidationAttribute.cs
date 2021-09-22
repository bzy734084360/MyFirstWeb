using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Utils.BlogValidation
{
    /// <summary>
    /// 自定义验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MyValidationAttribute : ValidationAttribute, IClientValidatable
    {
        public override string FormatErrorMessage(string name)
        {
            return "不等于bzy+自定义内容：" + name;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var result = new List<ModelClientValidationRule>();
            result.Add(new ModelClientValidationRule()
            {
                ErrorMessage = this.FormatErrorMessage("") + " 来自自定义客户端验证",
                ValidationType = "myvalidation"
            });
            return result;
        }
        public override bool IsValid(object value)
        {
            var val = value as string;
            if (string.IsNullOrEmpty(val) && val.Equals("bzy"))
                return true;
            return false;
        }
    }
}