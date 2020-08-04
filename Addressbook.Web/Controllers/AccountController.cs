using Addressbook.Core.Interface.Managers;
using Addressbook.Core.Models;
using Addressbook.Web.Models;
using Addressbook.Web.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Addressbook.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User, int> _user;

        public IAuthManager _authentication;


        public AccountController(IAccountManager account, IAuthManager authentication)
        {
            var store = new UserStore(account);
            _user = new UserManager<User, int>(store);
            _user.PasswordHasher = new MD5PasswordHasher();
            _authentication = authentication;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            try
            {
                var user = ValidateUser(model);
                //Sign User In
                var signIn = SignIn(user, model.RememberMe);
                //Redirect to Home
                return RedirectToAction("index", "home");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("LoginError", ex);
                return View(model);
            }
        }

        private User ValidateUser(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _user.Find(model.Email, model.Password);
                if (user == null) throw new Exception("Invalid Username or Password");
                return user;
            }
            else
            {
                var error = ModelState.Values.SelectMany(v => v.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .Aggregate((ag, e) => ag + ", " + e);
                throw new Exception(error);
            }

        }

        private ClaimsIdentity SignIn(User model, bool rememberMe)
        {
            var identity = _user.CreateIdentityAsync(model, DefaultAuthenticationTypes.ApplicationCookie).Result;

            //Optionally Add Additional Claims

            _authentication.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);

            return identity;
        }

        public ActionResult LogOut()
        {
            _authentication.SignOut();
            return RedirectToAction("login");
        }

        public ActionResult NotAuthorized()
        {
            return RedirectToAction("login");
        }

        public ActionResult SignUp()
        {
            return View();
        }
    }
}