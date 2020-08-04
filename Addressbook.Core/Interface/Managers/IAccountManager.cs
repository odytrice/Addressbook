using Addressbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook.Core.Interface.Managers
{
    public interface IAccountManager
    {
        UserModel CreateUser(UserModel user);
        void DeleteUser(UserModel user);
        UserModel UpdateUser(UserModel user);

        string GetPasswordHash(int userId);
        string SetPasswordHash(int userId, string passwordHash);


        void RemoveUserFromRole(UserModel user, string roleName);
        bool IsUserInRole(UserModel user, string roleName);
        IList<string> GetRoles(UserModel user);        
        UserModel FindByEmail(string userName);
        UserModel FindByID(int userId);
        PermissionModel[] GetPermissions(int userId);
    }
}
