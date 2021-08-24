using Bzy.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace NewStudy
{
    /// <summary>
    /// Forms认证主体
    /// </summary>
    public class Principal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public IUserData UserData { get; set; }

        public Principal(FormsAuthenticationTicket ticket, IUserData userData)
        {
            //EnsureHelper.EnsureNotNull(ticket, "ticket");
            //EnsureHelper.EnsureNotNull(userData, "userData");
            this.Identity = new FormsIdentity(ticket);
            this.UserData = userData;
        }

        public bool IsInRole(string role)
        {
            return this.UserData.IsInRole(role);
        }
        public bool IsInUser(string user)
        {
            return this.UserData.IsInUser(user);
        }
    }
}