using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5.Controllers
{
    public class MVC5StudyController : Controller
    {
        private List<OrderType> demoList1 =
            new List<OrderType>() {
                new OrderType(){Id=1,Type=1,TypeName="我是类型1" },
                new OrderType(){Id=2,Type=2,TypeName="我是类型2" },
                new OrderType(){Id=3,Type=3,TypeName="我是类型3" },
                new OrderType(){Id=4,Type=4,TypeName="我是类型4" },
                new OrderType(){Id=5,Type=5,TypeName="我是类型5" },
                new OrderType(){Id=6,Type=6,TypeName="我是类型6" },
                new OrderType(){Id=7,Type=7,TypeName="我是类型7" },
                new OrderType(){Id=8,Type=8,TypeName="我是类型8" }
            };

        #region 6章 数据注解和验证

        public ActionResult FormStudy()
        {
            ModelState.AddModelError("", "这是错误");
            return View();
        }
        /// <summary>
        /// 5.1程序清单demo
        /// </summary>
        /// <returns></returns>
        public ActionResult Search(string q)
        {
            var resultData = demoList1.Where(t => t.TypeName.Contains(q)).ToList();
            ViewBag.data = resultData;
            return View();
        }
        /// <summary>
        /// 修改demo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ModelState.AddModelError("TypeName", "这是错误的类型名称");
            return View(demoList1.Where(t => t.Id == id).ToList().FirstOrDefault());
        }



        /// <summary>
        /// 6章 数据注解和验证
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateOrder()
        {
            return View();
        }
        // POST: StoreManager/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(Order album)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            //var test = album.Validate();
            return View(album);
        }

        public JsonResult CheckUserName(string username)
        {
            var result = false;
            //Membership.FindUsersByName(username).Count == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 7章成员资格、授权、安全性



        #endregion

        #region 8 Ajax

        #endregion

        #region 9 路由

        /// <summary>
        /// 内敛约束
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public ActionResult Constraint(string year, string month, string day)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.Day = day;

            return View();
        }

        #endregion

        #region 10 WebAPI


        #endregion

        #region 13 依赖注入

        //控制反转

        #endregion

        #region 14单元测试



        #endregion
    }
}