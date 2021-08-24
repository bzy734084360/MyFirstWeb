using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.BizLogic
{
    /// <summary>
    /// 用户表业务接口
    /// </summary>
    public interface IBzyUserService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddEntity(BzyUserEntity entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdateEntity(BzyUserEntity entity);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BzyUserEntity QueryEntity(string id);
        /// <summary>
        /// token用户校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        Task<BzyUserEntity> QueryEntityAsync(string userName, string userPassword);
        /// <summary>
        /// 查询注册信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BzyUserEntity QueryEntityByRegister(string userName);
        /// <summary>
        /// 查询列表 根据标识集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<BzyUserEntity> QueryListByIds(string[] ids);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteEntity(string id);
    }
}
