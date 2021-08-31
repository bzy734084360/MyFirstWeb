using BlogModel;
using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        /// 
        /// </summary>
        public DbSet<Post> Posts { get; set; }
    }
}
