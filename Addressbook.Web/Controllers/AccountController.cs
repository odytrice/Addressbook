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

        public IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;


        public AccountController(IAccountManager account)
        {
            _user = new UserManager<User, int>(new UserStore(account));
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            var validateAndSignIn = from user in ValidateUser(model)
                                    from signIn in SignIn(user, model.RememberMe)
                                    select user;

            //TODO: Peform Validation
            var isValid = validateAndSignIn.Succeeded;
            //Sign User In
            if (isValid)
            {
                //Redirect to Home
                return RedirectToAction("index", "home");
            }
            else
            {
                return View(model);
            }
        }

        private Operation<User> ValidateUser(LoginModel model)
        {
            return Operation.Create(() =>
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
            });
        }

        private Operation<ClaimsIdentity> SignIn(User model, bool rememberMe)
        {
            return Operation.Create(() =>
            {
                var identity = _user.CreateIdentity(model, DefaultAuthenticationTypes.ApplicationCookie);

                //Optionally Add Additional Claims

                Authentication.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);

                return identity;
            });
        }

        public ActionResult LogOut()
        {
            Authentication.SignOut();
            return RedirectToAction("login");
        }
    }
}