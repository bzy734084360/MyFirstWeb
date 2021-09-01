using BlogModel;
using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepository
{
    public class MyBlogRepository
    {
        public List<Post> GetAll()
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
                return dbcontext.Posts.ToList();
            }
        }

        public Post GetById(int id)
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
                return dbcontext.Posts.Find(id);
            }
        }

        public void Update(Post post)
        {
            using (var dbcontext = new BlogContext())
            {
                dbcontext.Entry(post).State = System.Data.Entity.EntityState.Modified;
                dbcontext.SaveChanges();
            }
        }

        public void Insert(Post post)
        {
            using (var dbcontext = new BlogContext())
            {
                dbcontext.Posts.Add(post);
                dbcontext.SaveChanges();
            }
        }
        public void Delete(Post post)
        {
            using (var dbcontext = new BlogContext())
            {
                dbcontext.Entry(post).State = System.Data.Entity.EntityState.Deleted;
                //dbcontext.Posts.Remove(post);
                dbcontext.SaveChanges();
            }
        }
    }
}
