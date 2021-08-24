using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserData
    {
        /// <summary>
        /// 判断当前角色是否属于指定的角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool IsInRole(string role);
        /// <summary>
        /// 判断当前角色是否属于指定的角色用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsInUser(string user);
    }
}
