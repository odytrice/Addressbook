
using Addressbook.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Addressbook.Core.Interface.Managers;

namespace Addressbook.Web.Utils
{
    public class UserStore : IUserStore<User, int>, IUserPasswordStore<User, int>, IUserRoleStore<User, int>
    {
        private IAccountManager _account;

        public UserStore(IAccountManager account)
        {
            _account = account;
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(User user)
        {
            return _account.CreateUser(user).Pipe(Task.FromResult);
        }

        public Task DeleteAsync(User user)
        {
            _account.DeleteUser(user);
            return Task.FromResult(0);
        }

        public void Dispose()
        {
            
        }

        public Task<User> FindByIdAsync(int userId)
        {
            var user = _account.FindByID(userId);
            return Task.FromResult(new User(user));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var user = _account.FindByEmail(userName);
            return Task.FromResult(new User(user));
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            var passHash = _account.GetPasswordHash(user.UserId);
            return Task.FromResult(passHash);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            var getRoles = _account.GetRoles(user);
            return Task.FromResult(getRoles);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return _account.IsUserInRole(user, roleName).Pipe(Task.FromResult);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            _account.RemoveUserFromRole(user, roleName);
            return Task.FromResult(1);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return _account.SetPasswordHash(user.UserId, passwordHash).Pipe(Task.FromResult);
        }

        public Task UpdateAsync(User user)
        {
            return _account.UpdateUser(user).Pipe(Task.FromResult);
        }
    }
}