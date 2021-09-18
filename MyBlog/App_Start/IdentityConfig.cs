using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MyBlog.Identity;

namespace MyBlog
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入电子邮件服务可发送电子邮件。
            FileStream fs = new FileStream(@"D:\yf\tempData\EmailMsg.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(message.Subject);
            sw.WriteLine(message.Destination);
            sw.WriteLine(message.Body);
            sw.Close();
            fs.Close();
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // 在此处插入 SMS 服务可发送短信。
            FileStream fs = new FileStream(@"D:\yf\tempData\SmsMsg.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(message.Subject);
            sw.WriteLine(message.Destination);
            sw.WriteLine(message.Body);
            sw.Close();
            fs.Close();
            return Task.FromResult(0);
        }
    }

    // 配置此应用程序中使用的应用程序用户管理器。UserManager 在 ASP.NET Identity 中定义，并由此应用程序使用。
    public class BlogUserManager : UserManager<BlogUser>
    {
        public BlogUserManager(IUserStore<BlogUser> store)
            : base(store)
        {
        }

        public static BlogUserManager Create(IdentityFactoryOptions<BlogUserManager> options, IOwinContext context)
        {
            var manager = new BlogUserManager(new UserStore<BlogUser>(context.Get<BlogIdentityDbContext>()));
            // 配置用户名的验证逻辑
            manager.UserValidator = new UserValidator<BlogUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // 配置密码的验证逻辑
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // 配置用户锁定默认值
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // 注册双重身份验证提供程序。此应用程序使用手机和电子邮件作为接收用于验证用户的代码的一个步骤
            // 你可以编写自己的提供程序并将其插入到此处。
            manager.RegisterTwoFactorProvider("手机信息", new PhoneNumberTokenProvider<BlogUser>
            {
                MessageFormat = "您登录的验证码是{0}"
            });
            manager.RegisterTwoFactorProvider("邮件信息", new EmailTokenProvider<BlogUser>
            {
                Subject = "My Blog 登陆验证信息",
                BodyFormat = "您登录的验证码是{0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<BlogUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // 配置要在此应用程序中使用的应用程序登录管理器。
    public class ApplicationSignInManager : SignInManager<BlogUser, string>
    {
        public ApplicationSignInManager(BlogUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(BlogUser user)
        {
            return user.GenerateUserIdentityAsync((BlogUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<BlogUserManager>(), context.Authentication);
        }
    }
}
