using Addressbook.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Addressbook.Web.Utils;

namespace Addressbook.Web.Controllers
{
    [AuthorizeUser("Home-Page")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            int userID = User.Identity.GetUserId<int>();
            string email = User.Identity.GetUserName();

            var user = new User { UserId = userID, Email = email };
            return View(user);
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}