using Bzy.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.BizLogic
{
    /// <summary>
    /// 用户表业务实现
    /// </summary> 
    public class BzyUserService : IBzyUserService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddEntity(BzyUserEntity entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                //添加事务
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(@"INSERT INTO [dbo].[bzy_User]([Id],[UserName],[UserPassword],[ModifiedTime])
VALUES (@Id,@UserName,@UserPassword,@ModifiedTime)", entity);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;
                }
                return 1;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteEntity(string id)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Execute(@"delete bzy_User where Id=@Id", new { Id = id });
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateEntity(BzyUserEntity entity)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Execute(@"UPDATE [dbo].[bzy_User]
                                            SET 
                                            [UserName] =@UserName,
                                            [UserPassword] = @UserPassword,
                                            [ModifiedTime] = @ModifiedTime
                                            WHERE Id=@Id", entity);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BzyUserEntity QueryEntity(string id)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Query<BzyUserEntity>(@"select*from  bzy_User where Id=@Id", new { Id = id }).SingleOrDefault();
            }
        }
        /// <summary>
        /// token用户校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public Task<BzyUserEntity> QueryEntityAsync(string userName, string userPassword)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.QueryFirstAsync<BzyUserEntity>(@"select *from bzy_User where userName=@userName and userPassword=@userPassword", new { userName, userPassword });
            }
        }
        /// <summary>
        /// token用户校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public BzyUserEntity QueryEntity(string userName, string userPassword)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.QueryFirst<BzyUserEntity>(@"select *from bzy_User where userName=@userName and userPassword=@userPassword", new { userName, userPassword });
            }
        }

        /// <summary>
        /// 查询用户注册信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public BzyUserEntity QueryEntityByRegister(string userName)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Query<BzyUserEntity>(@"select *from  bzy_User where UserName=@UserName", new { UserName = userName }).SingleOrDefault();
            }
        }

        /// <summary>
        /// 查询集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<BzyUserEntity> QueryListByIds(string[] ids)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Query<BzyUserEntity>(@"select*from  bzy_User where Id=@Id", ids).ToList();
            }
        }
    }
}
