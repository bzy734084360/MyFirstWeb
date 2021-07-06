using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVC5.Models
{
    /// <summary>
    /// 订单类型模型
    /// </summary>
    public class OrderType
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 数字类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        [DisplayName("TypeName")]
        public string TypeName { get; set; }
    }
}