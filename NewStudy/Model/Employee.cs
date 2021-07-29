using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewStudy.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "请输入姓")]
        public string FirstName { get; set; }
        [StringLength(5, ErrorMessage = "名长度超过5个字符")]
        public string LastName { get; set; }

        public int? Salary { get; set; }
    }
}