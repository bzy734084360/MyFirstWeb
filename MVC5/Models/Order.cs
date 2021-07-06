using MVC5.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5.Models
{
    public class Order : IValidatableObject
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        [ScaffoldColumn(false)]
        [Remote("CheckUserName", "MVC5Study")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "名字不能为空")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "请输入正确的长度(3-20)")]
        [Display(Name = "姓氏", Order = 15001)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} 不能为空")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} 长度异常，请输入正确的长度")]
        [MaxWords(3, ErrorMessage = "{0}输入的数据异常")]
        [Display(Name = "名称", Order = 15000)]
        public string LastName { get; set; }
        public string Address { get; set; }

        [ReadOnly(true)]
        [HiddenInput]
        [Display(Name = "")]
        public string City { get; set; }

        [DataType(DataType.Password)]
        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "不是一个正确的邮箱地址")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Email")]
        public string EamilConfirm { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        [Range(typeof(decimal), "2.00", "49.99")]
        public decimal Total { get; set; }

        //public List<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LastName != null && LastName.Split(' ').Length > 10)
            {
                yield return new ValidationResult("我是模型对象验证", new[] { "LaseName" });
            }
        }
    }
}