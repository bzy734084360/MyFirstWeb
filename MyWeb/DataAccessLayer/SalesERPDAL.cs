using MyWeb.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyWeb.DataAccessLayer
{
    public class SalesERPDAL : DbContext
    {
        public SalesERPDAL() : base("StudayDal")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("TblEmployee");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
    }
}