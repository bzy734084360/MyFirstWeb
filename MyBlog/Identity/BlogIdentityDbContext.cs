using Bzy.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MyBlog.Identity
{
    public class BlogIdentityDbContext : IdentityDbContext<ApplicationUser>
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
        public static BlogIdentityDbContext Create()
        {
            return new BlogIdentityDbContext();
        }

    }
    public class ApplicationUser : IdentityUser
    {
        //public string Address { get; set; }

        //public string TwitterHandle { get; set; }

        public DateTime CreateOn { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }
}