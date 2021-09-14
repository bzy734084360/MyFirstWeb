using BlogBusinessLogic;
using BlogModel;
using MyBlog.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Areas.Admin.Controllers
{
    public class PostManagementController : Controller
    {
        private BlogManager manager = new BlogManager();
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var posts = manager.GetAllPosts().Select(t => new PostMaintainViewModel()
            {
                Content = t.Content,
                ID = t.ID,
                Title = t.Title
            }).ToList();
            var postListViewMolde = new PostMaintainListViewModel()
            {
                Count = posts.Count,
                PageCount = 1,
                Pages = 1,
                Posts = posts
            };
            return View(postListViewMolde);
        }
        public ActionResult Insert()
        {
            return View(new PostMaintainViewModel());
        }
        [HttpPost]
        public ActionResult Insert(PostMaintainViewModel model)
        {
            BlogPost entity = new BlogPost();
            entity.Author = "暴走鱼";
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;
            entity.Content = model.Content;
            entity.Title = model.Title;
            entity.IsPublish = false;
            manager.Insert(entity);
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var post = manager.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var postViewModel = new PostMaintainViewModel()
            {
                Content = post.Content,
                ID = post.ID,
                Title = post.Title
            };
            return View(postViewModel);
        }
        [HttpPost]
        public ActionResult Update(PostMaintainViewModel model)
        {
            var post = manager.GetPostById(model.ID);
            post.Content = model.Content;
            post.Title = model.Title;
            post.ModifyDate = DateTime.Now;
            manager.UpdatePost(post);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = manager.GetPostById(id);
            manager.Delete(post);
            return Content("1");
        }
    }
}