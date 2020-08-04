using Addressbook.Core.Interface.Managers;
using Addressbook.Core.Interface.Queries;
using Addressbook.Core.Managers;
using Addressbook.Infrastructure.Entities;
using Addressbook.Infrastructure.Queries;
using Microsoft.AspNet.Identity;
using Ninject.Modules;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Addressbook.Web.Models;
using Addressbook.Web.Utils;

namespace Addressbook.Web.Modules
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<DataContext>().InRequestScope();
            Bind<IAccountQueries>().To<AccountQueries>();
            Bind<IAccountManager>().To<AccountManager>();
            Bind<UserManager<Models.User, int>>().ToSelf();
            Bind<UserStore>().ToSelf();
            Bind<IAuthManager>().To<AuthManager>().InRequestScope();
        }
    }
}