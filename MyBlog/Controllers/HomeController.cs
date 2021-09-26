using BlogBusinessLogic;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : ControllerBase
    {
        private BlogPostManager manager;
        public HomeController(BlogPostManager blogManager)
        {
            manager = blogManager;
        }
        public ActionResult Index()
        {
            var postList = manager.GetTop5().Select(t => new PostViewModel
            {
                ID = t.ID,
                Title = t.Title,
                Content = t.Content,
                CreateDate = t.CreateDate,
                ModifyDate = t.ModifyDate,
                Author = t.Author
            }).ToList();
            return View(postList);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}