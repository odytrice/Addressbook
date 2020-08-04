using Addressbook.Core.Interface.Managers;
using Addressbook.Core.Interface.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addressbook.Core.Models;

namespace Addressbook.Core.Managers
{
    public class AccountManager : IAccountManager
    {
        private IAccountQueries _queries;

        public AccountManager(IAccountQueries queries)
        {
            _queries = queries;
        }

        public UserModel CreateUser(UserModel model)
        {
            //Validate User
            model.Validate();

            //Check to see if User already exists
            var user = _queries.GetUserById(model.UserId);
            if (user != null) throw new Exception("User Already Exists");

            var newUser = _queries.CreateUser(model);
            return newUser;
        }

        public void DeleteUser(UserModel user)
        {
            _queries.DeleteUser(user);
        }

        public UserModel FindByEmail(string userName)
        {
            return _queries.FindByEmail(userName);
        }

        public UserModel FindByID(int userId)
        {
            var user = _queries.GetUserById(userId);
            return user;
        }

        public string GetPasswordHash(int userId)
        {
            var user = _queries.GetUserById(userId);
            return user.Password;
        }

        public PermissionModel[] GetPermissions(int userId)
        {
            return _queries.GetPermissions(userId);
        }

        public IList<string> GetRoles(UserModel user)
        {
            var roles = _queries.GetRoles(user.UserId).Select(r => r.Name).ToList();
            return roles as IList<string>;
        }

        public bool IsUserInRole(UserModel user, string roleName)
        {
            var roles = _queries.GetRoles(user.UserId);
            return roles.Any(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }

        public void RemoveUserFromRole(UserModel user, string roleName)
        {
            var role = _queries.GetRoleByName(roleName);
            if (role == null) throw new Exception("Invalid Role Name");
            _queries.RemoveUserRole(user.UserId, role.RoleId);
        }

        public string SetPasswordHash(int userId, string passwordHash)
        {
            return _queries.UpdatePasswordHash(userId, passwordHash);
        }

        public UserModel UpdateUser(UserModel user)
        {
            return _queries.UpdateUser(user);
        }
    }
}
