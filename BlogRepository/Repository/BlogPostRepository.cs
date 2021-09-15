using BlogModel;
using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepository.Repository
{
    /// <summary>
    /// 博客文章仓库
    /// </summary>
    public class BlogPostRepository
    {
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<BlogPost> GetAll()
        {
            //代码操作
            //        string queryString = @"SELECT [ID],[Title],[Content],[CreateDate],[ModifyDate],[Author] 
            //FROM [dbo].[Posts] ";
            //        var result = new List<Post>();
            //        using (SqlConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            //        {
            //            SqlCommand command = new SqlCommand(queryString, connection);
            //            try
            //            {
            //                connection.Open();
            //                SqlDataReader reader = command.ExecuteReader();
            //                while (reader.Read())
            //                {
            //                    result.Add(new Post()
            //                    {
            //                        Author = reader["Author"].ToString(),
            //                        Content = reader["Content"].ToString(),
            //                        CreateDate = DateTime.Parse(reader["CreateDate"].ToString()),
            //                        ModifyDate = DateTime.Parse(reader["ModifyDate"].ToString()),
            //                        ID = int.Parse(reader["ID"].ToString()),
            //                        Title = reader["Title"].ToString()
            //                    });
            //                }
            //                reader.Close();
            //            }
            //            catch (Exception)
            //            {
            //            }
            //        }
            //        return result;
            using (var dbcontext = new BlogContext())
            {
                return dbcontext.BlogPost.ToList();
            }
        }
        /// <summary>
        /// 获取指定文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogPost GetById(int id)
        {
            //代码操作
            //        string queryString = @"SELECT [ID],[Title],[Content],[CreateDate],[ModifyDate],[Author] 
            //FROM [dbo].[Posts] where [ID]=@id";
            //        var result = new Post();
            //        using (SqlConnection connection = new SqlConnection(SystemInfo.BzyDbConnection))
            //        {
            //            SqlCommand command = new SqlCommand(queryString, connection);
            //            command.Parameters.AddWithValue("@id", id);
            //            try
            //            {
            //                connection.Open();
            //                SqlDataReader reader = command.ExecuteReader();
            //                while (reader.Read())
            //                {
            //                    result = new Post()
            //                    {
            //                        Author = reader["Author"].ToString(),
            //                        Content = reader["Content"].ToString(),
            //                        CreateDate = DateTime.Parse(reader["CreateDate"].ToString()),
            //                        ModifyDate = DateTime.Parse(reader["ModifyDate"].ToString()),
            //                        ID = int.Parse(reader["ID"].ToString()),
            //                        Title = reader["Title"].ToString()
            //                    };
            //                }
            //                reader.Close();
            //            }
            //            catch (Exception)
            //            {
            //            }
            //        }
            //        return result;
            using (var dbcontext = new BlogContext())
            {
                return dbcontext.BlogPost.Find(id);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="post"></param>
        public void Update(BlogPost post)
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
        public void Insert(BlogPost post)
        {
            using (var dbcontext = new BlogContext())
            {
                dbcontext.BlogPost.Add(post);
                dbcontext.SaveChanges();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="post"></param>
        public void Delete(BlogPost post)
        {
            using (var dbcontext = new BlogContext())
            {
                dbcontext.Entry(post).State = System.Data.Entity.EntityState.Deleted;
                dbcontext.SaveChanges();
            }
        }
    }
}
