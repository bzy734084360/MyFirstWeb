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
        public BlogIdentityDbContext() : base(SystemInfo.BzyDbConnection, throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogIdentityDbContext, Migrations.Configuration>());
            //添加主键
            modelBuilder.Entity<IdentityUserRole>().HasKey(t => t.RoleId).HasKey(t => t.UserId);
            modelBuilder.Entity<IdentityUserLogin>().HasKey(t => t.UserId);

        }

    }
}