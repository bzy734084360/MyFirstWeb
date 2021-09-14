using BlogModel;
using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogRepository
{
    /// <summary>
    /// EF
    /// </summary>
    public class BlogContext : DbContext
    {
        public BlogContext() : base(SystemInfo.BzyDbConnection)
        {

        }
        /// <summary>
        /// 更新Model 操作
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Migrations.Configuration>());
            ////添加主键
            //modelBuilder.Entity<Post>().HasKey(t => t.ID);
            ////设置列属性
            //modelBuilder.Entity<Post>().Property(t => t.Title).HasMaxLength(255);
            ////创建索引(ef6.1版本支持)
            //modelBuilder.Entity<Post>().Property(t => t.Title).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute()));
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<BlogPost> BlogPost { get; set; }
    }
}
