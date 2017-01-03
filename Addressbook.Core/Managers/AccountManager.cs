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

        public Operation<UserModel> CreateUser(UserModel model)
        {
            return Operation.Create(() =>
            {
                //Validate User
                model.Validate().Throw();

                //Check to see if User already exists
                var user = _queries.GetUserById(model.UserId);
                if (user != null) throw new Exception("User Already Exists");

                var newUser = _queries.CreateUser(model);
                return newUser;
            });
        }

        public Operation DeleteUser(UserModel user)
        {
            return Operation.Create(() =>
            {
                _queries.DeleteUser(user);
            });
        }

        public Operation<UserModel> FindByEmail(string userName)
        {
            return Operation.Create(() =>
            {
                return _queries.FindByEmail(userName);
            });
        }

        public Operation<UserModel> FindByID(int userId)
        {
            return Operation.Create(() =>
            {
                return _queries.GetUserById(userId);
            });
        }

        public Operation<string> GetPasswordHash(int userId)
        {
            return Operation.Create(() =>
            {
                var user = _queries.GetUserById(userId);
                return user.Password;
            });
        }

        public Operation<PermissionModel[]> GetPermissions(int userId)
        {
            return Operation.Create(() =>
            {
                return _queries.GetPermissions(userId);
            });
        }

        public Operation<IList<string>> GetRoles(UserModel user)
        {
            return Operation.Create(() =>
            {
                return _queries.GetRoles(user.UserId).ToList() as IList<string>;
            });
        }

        public Operation<bool> IsUserInRole(UserModel user, string roleName)
        {
            return Operation.Create(() =>
            {
                var roles = _queries.GetRoles(user.UserId);
                return roles.Any(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
            });
        }

        public Operation RemoveUserFromRole(UserModel user, string roleName)
        {
            return Operation.Create(() =>
            {
                var role = _queries.GetRoleByName(roleName);
                if (role == null) throw new Exception("Invalid Role Name");
                _queries.RemoveUserRole(user.UserId, role.RoleId);
            });
        }

        public Operation<string> SetPasswordHash(int userId, string passwordHash)
        {
            return Operation.Create(() =>
            {
                return _queries.UpdatePasswordHash(userId, passwordHash);
            });
        }

        public Operation<UserModel> UpdateUser(UserModel user)
        {
            return Operation.Create(() =>
            {
                return _queries.UpdateUser(user);
            });
        }
    }
}
