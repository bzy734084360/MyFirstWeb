using BlogModel;
using Bzy.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepository
{
    /// <summary>
    /// 博客Client仓库
    /// </summary>
    public class BlogClientRepository
    {
        /// <summary>
        /// 获取指定博客Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogClient GetById(int id)
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
        public void Update(BlogClient post) 
        {
            using (var dbcontext = new BlogContext())
            {
                dbcontext.Entry(post).State = System.Data.Entity.EntityState.Modified;
                dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="post"></param>
        public void Insert(BlogClient post)
        {
            using (var dbcontext = new BlogContext())
            {
                //dbcontext.BlogPost.Add(post);
                //dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="post"></param>
        public void Delete(BlogClient post)
        {
            using (var dbcontext = new BlogContext())
            {
                dbcontext.Entry(post).State = System.Data.Entity.EntityState.Deleted;
                dbcontext.SaveChanges();
            }
        }
    }
}
