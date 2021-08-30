using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class PostController : Controller
    {
        private static List<Post> posts = new List<Post>()
        {
            new Post(){
                Id=1,Author="暴走鱼",
                Content="文章1的内容",
                CreateDate=DateTime.Now,
                ModifyDate=DateTime.Now,
                Title="文章1"
            },
            new Post(){
                Id=1,Author="暴走鱼",
                Content="文章2的内容",
                CreateDate=DateTime.Now,
                ModifyDate=DateTime.Now,
                Title="文章2"
            },
            new Post(){
                Id=1,Author="暴走鱼",
                Content="文章3的内容",
                CreateDate=DateTime.Now,
                ModifyDate=DateTime.Now,
                Title="文章3"
            },
            new Post(){
                Id=1,Author="暴走鱼",
                Content="文章4的内容",
                CreateDate=DateTime.Now,
                ModifyDate=DateTime.Now,
                Title="文章4"
            },
            new Post(){
                Id=1,Author="暴走鱼",
                Content="文章5的内容",
                CreateDate=DateTime.Now,
                ModifyDate=DateTime.Now,
                Title="文章5"
            },
        };
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Posts = posts;
            return View();
        }
        /// <summary>
        /// 文章详情
        /// </summary>
        /// <returns></returns>
        public ActionResult PostDetail(int id)
        {
            var post = posts.Where(t => t.Id == id).FirstOrDefault();
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Post = post;
            return View();
        }
    }
}