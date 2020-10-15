using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyWeb.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FistName { get; set; }

        public string LastName { get; set; }

        public int Salary { get; set; }
    }
}