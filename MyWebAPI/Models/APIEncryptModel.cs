using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebAPI.Models
{
    /// <summary>
    /// 密文Model
    /// </summary>
    public class APIEncryptModel
    {
        /// <summary>
        /// ase加密数据
        /// </summary>
        public string aesdata { get; set; } = string.Empty;
        /// <summary>
        /// aes加密数据
        /// </summary>
        public string aeskey { get; set; } = string.Empty;
        /// <summary>
        /// 加密类型（1 IOS 2 安卓）
        /// </summary>
        public int encryptType { get; set; } = 1;
    }

    /// <summary>
    /// 密文Model
    /// </summary>
    public class APIEncryptOutModel
    {
        /// <summary>
        /// rsa加密数据
        /// </summary>
        public string aesdata { get; set; } = string.Empty;
        /// <summary>
        /// rsa加密数据
        /// </summary>
        public string aeskey { get; set; } = string.Empty;
    }
}