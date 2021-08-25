using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebAPI.Areas.GrumpyFish
{
    /// <summary>
    /// 注册用户入参
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
    }
}