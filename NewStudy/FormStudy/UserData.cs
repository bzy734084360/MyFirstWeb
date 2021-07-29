using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewStudy
{
    /// <summary>
    /// 定义用户对象进行数据存储
    /// </summary>
    public class UserData : IUserData
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                return true;
            }
            return role.Split(',').Any(t => t.Equals(this.UserRole, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsInUser(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                return true;
            }
            return user.Split(',').Any(item => item.Equals(this.UserName, StringComparison.OrdinalIgnoreCase));
        }
    }
}