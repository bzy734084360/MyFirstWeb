using Bzy.Utilities;
using MyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyWebAPI.App_Start.Filter
{
    /// <summary>
    /// 处理加密数据
    /// </summary>
    public class DataDecryptAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 是否解密签名处理
        /// </summary>
        public bool IsCheckSign { get; set; } = false;
        /// <summary>
        /// 出参加密
        /// </summary>
        public bool IsOutCheckSign { get; set; } = false;

        //private Log _logger;
        ///// <summary>
        ///// 日志操作
        ///// </summary>
        //public Log Logger
        //{
        //    get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        //}

        /// <summary>
        /// 调用前
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (IsCheckSign)
            {
                try
                {
                    APIEncryptModel model = null;
                    if (actionContext.ActionArguments.Count != 1)
                    {
                        throw new ApiCustomException("请求失败", 120);
                    }
                    foreach (var item in actionContext.ActionArguments)
                    {
                        string jsondata = JsonHelper.ToApiJson(item.Value);
                        model = JsonHelper.JsonToEntity<APIEncryptModel>(jsondata);
                    }
                    actionContext.Request.Properties.Add("encryptType", model.encryptType);
                    if (!string.IsNullOrEmpty(model.aeskey) && !string.IsNullOrEmpty(model.aesdata))
                    {
                        if (model.encryptType == 1)
                        {
                            //IOS 16位的key  
                            model.aeskey = SecretHelper.UnRsa(model.aeskey);
                            model.aesdata = SecretHelper.AESDecryptByData(model.aesdata, model.aeskey);
                        }
                        else if (model.encryptType == 2)
                        {
                            //安卓32位的key
                            model.aeskey = SecretHelper.UnRsaToJava(model.aeskey);
                            model.aesdata = SecretHelper.AESDecryptByData(model.aesdata, model.aeskey);
                        }
                        actionContext.ActionArguments.Remove("encryptModel");
                        actionContext.ActionArguments.Add("encryptModel", model);
                    }
                }
                catch (Exception ex)
                {
                    //Logger.Info("接口验签异常：" + ex.ToString());
                }
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
        /// <summary>
        /// 调用结束
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (IsOutCheckSign)
            {
                try
                {
                    string encryptType = actionExecutedContext.Request.Properties["encryptType"].ToString();
                    string getAesKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 16);
                    APIEncryptOutModel resultModel = new APIEncryptOutModel();
                    if (actionExecutedContext.Exception is ApiCustomException)
                    {
                        object result = null;
                        var exception = (ApiCustomException)actionExecutedContext.Exception;
                        result = new
                        {
                            success = false,
                            code = exception.ErrorCode,
                            message = actionExecutedContext.Exception.Message,
                        };
                        resultModel.aesdata = SecretHelper.AESEncryptByData(result.ToApiJson(), getAesKey);
                    }
                    else
                    {
                        var task = actionExecutedContext.Response.Content.ReadAsStringAsync();
                        var txt = task.Result;
                        resultModel.aesdata = SecretHelper.AESEncryptByData(txt, getAesKey);
                    }
                    if (encryptType == "1")
                    {
                        resultModel.aeskey = SecretHelper.Rsa(getAesKey);
                    }
                    else if (encryptType == "2")
                    {
                        resultModel.aeskey = SecretHelper.RsaToJava(getAesKey);
                    }
                    actionExecutedContext.Response = new HttpResponseMessage { Content = new StringContent(resultModel.ToApiJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
                }
                catch (Exception ex)
                {
                    //Logger.Info("验签返回异常：" + ex.ToString());
                }
            }
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}