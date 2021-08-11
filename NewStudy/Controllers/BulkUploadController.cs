using NewStudy.App_Start;
using NewStudy.BusinessLayer;
using NewStudy.Model;
using NewStudy.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NewStudy.Controllers
{
    public class BulkUploadController : AsyncController
    {
        // GET: BulkUpload
        [HeaderFooterFilter]
        [AdminFilter]
        public ActionResult Index()
        {
            return View(new FileUploadViewModel());
        }
        /// <summary>
        /// 异步上传文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AdminFilter]
        public async Task<ActionResult> Upload(FileUploadViewModel model)
        {
            List<Employee> employees = await Task.Factory.StartNew(() => GetEmployees(model));
            EmployeeBusinessLayer bal = new EmployeeBusinessLayer();
            bal.UploadEmployees(employees);
            return RedirectToAction("Index", "Employee");
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<Employee> GetEmployees(FileUploadViewModel model)
        {
            List<Employee> employees = new List<Employee>();
            StreamReader csvreader = new StreamReader(model.fileUpload.InputStream);
            csvreader.ReadLine();
            while (!csvreader.EndOfStream)
            {
                var line = csvreader.ReadLine();
                var values = line.Split(',');
                Employee e = new Employee();
                e.FirstName = values[0];
                e.LastName = values[1];
                e.Salary = int.Parse(values[2]);
                employees.Add(e);
            }
            return employees;
        }
    }
}