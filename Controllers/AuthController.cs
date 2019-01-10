using AuthnAuthz.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NakedIdentity.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AuthnAuthz.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly UserManager<App_User> userManager;


        public AuthController()
            : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public AuthController(UserManager<App_User> userManager)
        {
            this.userManager = userManager;
        }
        // GET: Auth
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await userManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                var identity = await userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }
            else
            {
                return Url.Action("SelectBook", "BookStore");
            }
        }
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new App_User
            {
                UserName = model.Email,
                Country = model.Country,
                Age = model.Age
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && userManager != null)
            {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }
        private async Task SignIn(App_User user)
        {
            var identity = await userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);

            identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));

            GetAuthenticationManager().SignIn(identity);
        }
        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }
    }
    
}