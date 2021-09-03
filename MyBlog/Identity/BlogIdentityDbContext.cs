using Bzy.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBlog.Identity
{
    public class BlogIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public BlogIdentityDbContext() : base(SystemInfo.BzyDbConnection)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogIdentityDbContext, Migrations.Configuration>());
            //Database.SetInitializer();
            ////添加主键
            //modelBuilder.Entity<Post>().HasKey(t => t.ID);
        }
    }
}