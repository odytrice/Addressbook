using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Addressbook.Web
{
    public interface IAuthManager
    {

        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);

        void SignIn(params ClaimsIdentity[] identities);

        void SignOut(AuthenticationProperties properties, params string[] authenticationTypes);

        void SignOut(params string[] authenticationTypes);
    }

    public class AuthManager : IAuthManager
    {
        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            HttpContext.Current.GetOwinContext().Authentication.SignIn(properties, identities);
        }

        public void SignIn(params ClaimsIdentity[] identities)
        {
            HttpContext.Current.GetOwinContext().Authentication.SignIn(identities);
        }

        public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(properties, authenticationTypes);
        }

        public void SignOut(params string[] authenticationTypes)
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(authenticationTypes);
        }
    }
}