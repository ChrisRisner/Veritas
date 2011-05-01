using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Veritas.DataLayer;
using Veritas.DataLayer.Models;
using Veritas.BusinessLayer.Caching;

namespace Veritas.BusinessLayer.Providers
{
    public class VeritasRoleManager : RoleProvider
    {
        VeritasRepository repo = VeritasRepository.GetInstance();

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            BlogUser[] users = repo.GetBlogUsersInRoles(CacheHandler.BlogConfigId, roleName);
            var v = from user in users
                    where user.Username.Contains(usernameToMatch)
                    select user.Username;
            return v.ToArray();

        }

        public override string[] GetAllRoles()
        {
            BlogRole[] roles = repo.GetBlogRoles(CacheHandler.BlogConfigId).ToArray();
            return (roles.Select(p => p.RoleName).ToArray());
        }

        public override string[] GetUsersInRole(string roleName)
        {
            BlogUser[] users = repo.GetBlogUsersInRoles(CacheHandler.BlogConfigId, roleName).ToArray();
            return (users.Select(p => p.Username).ToArray());
        }

        public override bool RoleExists(string roleName)
        {
            if (repo.GetBlogRoles(CacheHandler.BlogConfigId)
                .Where(p => p.RoleName == roleName).SingleOrDefault() != null)
                return true;
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            BlogRole[] roles = repo.GetRolesForUser(CacheHandler.BlogConfigId, username).ToArray();
            return (roles.Select(p => p.RoleName).ToArray());
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            BlogRole[] roles = repo.GetRolesForUser(CacheHandler.BlogConfigId, username).ToArray();
            if (roles.Where(p => p.RoleName == roleName).SingleOrDefault() != null)
                return true;
            return false;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
