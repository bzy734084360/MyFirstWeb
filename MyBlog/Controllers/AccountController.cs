using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MyBlog.Identity;
using MyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName };
                var userManger = new UserManager<IdentityUser, string>(new UserStore<IdentityUser>(new BlogIdentityDbContext()));
                var result = await userManger.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();

        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userManager = new UserManager<IdentityUser, string>(new UserStore<IdentityUser>(new BlogIdentityDbContext()));
            var sigInManager = new SignInManager<IdentityUser, string>(userManager, this.HttpContext.GetOwinContext().Authentication);
            var result = await sigInManager.PasswordSignInAsync(model.UserName, model.Password, false, shouldLockout: false);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}