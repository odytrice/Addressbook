using Addressbook.Core.Interface.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addressbook.Core.Models;
using Addressbook.Infrastructure.Entities;
using System.Data.Entity;

namespace Addressbook.Infrastructure.Queries
{
    public class AccountQueries : IAccountQueries
    {
        private DbContext _db;

        public AccountQueries(DbContext db)
        {
            _db = db;
        }

        public UserModel CreateUser(UserModel model)
        {
            var user = new User
            {
                Email = model.Email,
                Password = model.Password,
            };

            _db.Set<User>().Add(user);

            _db.SaveChanges();

            model.UserId = user.UserId;

            return model;
        }

        public void DeleteUser(UserModel model)
        {
            var user = _db.Set<User>().Find(model.UserId);
            if (user != null) _db.Set<User>().Remove(user);
            _db.SaveChanges();
        }

        public UserModel FindByEmail(string email)
        {
            var user = _db.Set<User>().Where(u => u.Email == email).FirstOrDefault();
            var model = new UserModel().Assign(user);
            return model;
        }

        public PermissionModel[] GetPermissions(int userId)
        {
            var query = from rolePermission in _db.Set<RolePermission>()
                        join userRole in _db.Set<UserRole>()
                        on rolePermission.RoleID equals userRole.RoleID
                        where userRole.UserId == userId
                        select new PermissionModel
                        {
                            PermissionID = rolePermission.PermissionID,
                            Name = rolePermission.Permission.Name,
                        };
            return query.ToArray();

        }

        public RoleModel GetRoleByName(string roleName)
        {
            throw new NotImplementedException();
        }

        public RoleModel[] GetRoles(int userID)
        {
            var query = from userRole in _db.Set<UserRole>()
                        where userRole.UserId == userID
                        select userRole.Role;
            var roles = query.ToArray();

            return roles.Select(r => new RoleModel().Assign(r)).ToArray();
        }

        public UserModel GetUserById(int userId)
        {
            var user = _db.Set<User>().Find(userId);
            var model = new UserModel().Assign(user);
            return model;
        }

        public void RemoveUserRole(int userId, int roleID)
        {
            var query = from userRoles in _db.Set<UserRole>()
                        where userRoles.UserId == userId
                        where userRoles.RoleID == roleID
                        select userRoles;

            var userRole = query.FirstOrDefault();
            if(userRole != null) 
            {
                _db.Set<UserRole>().Remove(userRole);
                _db.SaveChanges();
            }
        }

        public string UpdatePasswordHash(int userId, string passwordHash)
        {
            var user = _db.Set<User>().Find(userId);
            if (user == null) throw new Exception("Invalid User Id");
            user.Password = passwordHash;
            _db.SaveChanges();
            return user.Password;
        }

        public UserModel UpdateUser(UserModel model)
        {
            var user = _db.Set<User>().Find(model.UserId);
            if (user == null) throw new Exception("Invalid User");

            user.Email = model.Email;
            //For Security Update User Should not Update the Password
            _db.SaveChanges();

            var newUser = new UserModel().Assign(user);

            //Remove Password
            newUser.Password = null;
            return newUser;
        }
    }
}
