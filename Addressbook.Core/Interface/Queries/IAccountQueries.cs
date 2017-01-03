using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addressbook.Core.Models;

namespace Addressbook.Core.Interface.Queries
{
    public interface IAccountQueries
    {
        UserModel CreateUser(UserModel user);
        void DeleteUser(UserModel user);
        UserModel FindByEmail(string email);
        UserModel GetUserById(int userId);
        PermissionModel[] GetPermissions(int userId);
        RoleModel[] GetRoles(int userID);
        string UpdatePasswordHash(int userId, string passwordHash);
        void RemoveUserRole(int userId, int roleId);
        UserModel UpdateUser(UserModel user);
        RoleModel GetRoleByName(string roleName);
    }
}
