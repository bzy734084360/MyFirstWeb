using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBusinessLogic
{
    public interface IManagerBase<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddEntity(T entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UpdateEntity(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteEntity(string id);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T QueryEntity(string id);
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        List<T> QueryList();
    }
}
