using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Utilities
{
    /// <summary>
    /// 自定义API异常信息
    /// </summary>
    public class ApiCustomException : Exception
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        public int ErrorCode { get; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; }
        /// <summary>
        /// 拓展信息
        /// </summary>
        public string ExtMsg { get; set; }

        public ApiCustomException() { }
        /// <summary>
        /// 异常初始化
        /// </summary>
        /// <param name="errormsg"></param>
        /// <param name="errorcode"></param>
        /// <param name="extmsg"></param>
        public ApiCustomException(string errormsg, int errorcode = 100, string extmsg = "") : base(errormsg)
        {
            this.ErrorMsg = errormsg;
            this.ErrorCode = errorcode;
            this.ExtMsg = extmsg;
        }

    }
}
