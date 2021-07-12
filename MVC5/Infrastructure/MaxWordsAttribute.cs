using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Infrastructure
{
    /// <summary>
    /// 自定义注解
    /// </summary>
    public class MaxWordsAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly int _maxWords;
        public MaxWordsAttribute(int maxWords) : base("{0} 超出最大长度")
        {
            _maxWords = maxWords;
        }
        /// <summary>
        /// 服务端验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > _maxWords)
                {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// 客户端验证
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule();
            rule.ErrorMessage = FormatErrorMessage(metadata.GetDisplayName());
            rule.ValidationParameters.Add("wordcount", _maxWords);
            rule.ValidationType = "maxwords";
            yield return rule;
        }
    }
}