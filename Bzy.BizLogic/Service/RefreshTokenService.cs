using Bzy.BizLogic.BizEntity;
using Bzy.BizLogic.IService;
using Bzy.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.BizLogic.Service
{
    public class RefreshTokenService : IRefreshTokenService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> AddEntityAsync(RefreshTokenEntity entity)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.ExecuteAsync("insert", entity);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RefreshTokenEntity> QueryEntityAsync(string id)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.QueryFirstAsync<RefreshTokenEntity>("select *from  RefreshToken where id=@Id", new RefreshTokenEntity() { Id = id });
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DeleteEntityAsync(string id)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.ExecuteAsync("delete ", new RefreshTokenEntity() { Id = id });
            }
        }
    }
}
