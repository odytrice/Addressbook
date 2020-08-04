using Addressbook.Core.Interface.Managers;
using Addressbook.Core.Models;
using Addressbook.Web;
using Addressbook.Web.Controllers;
using Addressbook.Web.Models;
using Addressbook.Web.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Addressbook.Tests.ControllerTests
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void UserWithValidCredentialsCanLogin()
        {
            //Arrange
            var model = new LoginModel
            {
                Email = "someone@example.com",
                Password = "12345",
                RememberMe = true
            };

            var user = new UserModel
            {
                Email = "someone@example.com",
                Password = new MD5PasswordHasher().HashPassword("12345"),
                UserId = 1
            };

            var mockAccountManager = new Mock<IAccountManager>();
            mockAccountManager.Setup(m => m.FindByEmail(user.Email)).Returns(user);
            mockAccountManager.Setup(m => m.GetPasswordHash(user.UserId)).Returns(user.Password);
            mockAccountManager.Setup(m => m.GetRoles(It.IsAny<UserModel>())).Returns(new List<string>());

            var mockAuthentication = new Mock<IAuthManager>();
            //Act
            var sut = new AccountController(mockAccountManager.Object, mockAuthentication.Object);
            var result = sut.Login(model, "/");

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void UserWithInvalidCredentialsShouldNotLogin()
        {
            //Arrange
            var model = new LoginModel
            {
                Email = "someone@example.com",
                Password = "abcd",
                RememberMe = true
            };

            var user = new UserModel
            {
                Email = "someone@example.com",
                Password = new MD5PasswordHasher().HashPassword("12345"),
                UserId = 1
            };

            var mockAccountManager = new Mock<IAccountManager>();
            mockAccountManager.Setup(m => m.FindByEmail(user.Email)).Returns(user);
            mockAccountManager.Setup(m => m.GetPasswordHash(user.UserId)).Returns(user.Password);
            mockAccountManager.Setup(m => m.GetRoles(It.IsAny<UserModel>())).Returns(new List<string>());

            var mockAuthentication = new Mock<IAuthManager>();
            //Act
            var sut = new AccountController(mockAccountManager.Object, mockAuthentication.Object);
            var result = sut.Login(model, "/");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
