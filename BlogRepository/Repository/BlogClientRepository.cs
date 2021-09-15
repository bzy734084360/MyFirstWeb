using BlogModel;
using BlogRepository.IRepository;
using Bzy.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepository.Repository
{
    /// <summary>
    /// 博客Client仓库
    /// </summary>
    public class BlogClientRepository : IBlogClientRepository<BlogClient>
    {
        /// <summary>
        /// 获取指定博客Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogClient QueryEntity(string id)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Query<BlogClient>(@"select * from  BlogClient where Id=@Id", new { Id = id }).SingleOrDefault();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="post"></param>
        public int UpdateEntity(BlogClient entity)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Execute(@"UPDATE [dbo].[BlogClient]
   SET [Secret] =@Secret
      ,[RedirectUrl] =@RedirectUrl
      ,[ModifiedTime] = <ModifiedTime, datetime,>
 WHERE Id=@Id", entity);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="post"></param>
        public int AddEntity(BlogClient entity)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Execute(@"INSERT INTO [dbo].[BlogClient]
           ([Secret]
           ,[RedirectUrl]
           ,[CreateTime]
           ,[ModifiedTime])
     VALUES
           (@Secret
           ,@RedirectUrl
           ,@CreateTime
           ,@ModifiedTime)", entity);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="post"></param>
        public int DeleteEntity(string id)
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Execute(@"delete bzy_User where Id=@Id", new { Id = id });
            }
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public List<BlogClient> QueryList()
        {
            using (IDbConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            {
                return connection.Query<BlogClient>(@"select * from  BlogClient ").ToList();
            }
        }
    }
}
