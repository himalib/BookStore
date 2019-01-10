using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using NakedIdentity.Mvc;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace AuthnAuthz
{
    public class Startup
    {
        public static Func<UserManager<App_User>> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            // this is the same as before
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/auth/login")
            });

            // configure the user manager
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<App_User>(
                    new UserStore<App_User>(new AppDbContext()));
                // allow alphanumeric characters in username
                usermanager.UserValidator = new UserValidator<App_User>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };

                usermanager.ClaimsIdentityFactory = new AppUserClaimsIdentityFactory();

                return usermanager;
            };
        }
    }
}
