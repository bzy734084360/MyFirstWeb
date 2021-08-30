using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeb.Identity
{
    /// <summary>
    /// 用户DB
    /// </summary>
    public class MyWebIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public MyWebIdentityDbContext() : base("BzyDbConnection", throwIfV1Schema: false)
        {

        }
    }
}