using BlogBusinessLogic;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    /// <summary>
    /// 博客文章相关
    /// </summary>
    public class PostController : Controller
    {
        private BlogPostManager manager;
        public PostController(BlogPostManager blogManager)
        {
            //通过autofac 注入
            manager = blogManager;
        }
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var posts = manager.GetAllPosts().Select(t => new PostViewModel()
            {
                Author = t.Author,
                Content = t.Content,
                CreateDate = t.CreateDate,
                ID = t.ID,
                ModifyDate = t.ModifyDate,
                Title = t.Title
            }).ToList();
            var postListViewModel = new PostListViewModel()
            {
                Count = posts.Count,
                PageCount = 1,
                Pages = 1,
                Posts = posts
            };
            return View(postListViewModel);
        }
        /// <summary>
        /// 文章详情
        /// </summary>
        /// <returns></returns>
        public ActionResult PostDetail(int id)
        {
            var post = manager.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var postViewModel = new PostViewModel()
            {
                Author = post.Author,
                Content = post.Content,
                CreateDate = post.CreateDate,
                ModifyDate = post.ModifyDate,
                ID = post.ID,
                Title = post.Title
            };
            return View(postViewModel);
        }
    }
}