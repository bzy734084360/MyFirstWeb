using Bzy.BizLogic.BizEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.BizLogic.IService
{
    /// <summary>
    /// 刷新Token Service
    /// </summary>
    public interface IRefreshTokenService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddEntityAsync(RefreshTokenEntity entity);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RefreshTokenEntity> QueryEntityAsync(string id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteEntityAsync(string id);
    }
}
