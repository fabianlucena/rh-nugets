﻿using RFAuth.IServices;
using RFOperators;
using RFRBAC.Entities;
using RFRBAC.IServices;
using RFService.IRepo;
using RFService.Repo;
using RFService.Services;

namespace RFRBAC.Services
{
    public class UserRoleService(
        IRepo<UserRole> repo,
        IRoleParentService roleParentService,
        IUserService userService,
        IRoleService roleService
    ) : ServiceTimestamps<IRepo<UserRole>, UserRole>(repo),
            IUserRoleService
    {
        public async Task<IEnumerable<Int64>> GetRolesIdAsync(QueryOptions options)
        {
            var roles = await GetListAsync(options);
            return roles.Select(i => i.RoleId);
        }

        public async Task<IEnumerable<Int64>> GetRolesIdForUserIdAsync(Int64 userId, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("UserId", userId);

            return await GetRolesIdAsync(options);
        }

        public async Task<IEnumerable<Int64>> GetRolesIdForUsersIdAsync(IEnumerable<Int64> usersId, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("UserId", usersId);

            return await GetRolesIdAsync(options);
        }

        public async Task<IEnumerable<Int64>> GetAllRolesIdForUserIdAsync(Int64 userId, QueryOptions? options = null)
        {
            var rolesId = await GetRolesIdForUserIdAsync(userId, options);
            var allRolesId = await roleParentService.GetAllRolesIdForRolesIdAsync(rolesId);

            return allRolesId;
        }

        public async Task<IEnumerable<Int64>> GetAllRolesIdForUsersIdAsync(IEnumerable<Int64> usersId, QueryOptions? options = null)
        {
            var rolesId = await GetRolesIdForUsersIdAsync(usersId, options);
            var allRolesId = await roleParentService.GetAllRolesIdForRolesIdAsync(rolesId);

            return allRolesId;
        }

        public async Task<IEnumerable<Role>> GetRolesForUserIdAsync(long userId, QueryOptions? options)
        {
            var allRolesId = await GetRolesIdForUserIdAsync(userId);

            options ??= new QueryOptions();
            options.AddFilter("Id", allRolesId);
            return await roleService.GetListAsync(options);
        }

        public async Task<IEnumerable<Role>> GetAllRolesForUserIdAsync(long userId, QueryOptions? options = null)
        {
            var allRolesId = await GetAllRolesIdForUserIdAsync(userId);

            options ??= new QueryOptions();
            options.AddFilter("Id", allRolesId);
            return await roleService.GetListAsync(options);
        }

        public async Task UpdateRolesNameForUserNameAsync(
            IEnumerable<string> rolesName,
            string username
        )
        {
            var userId = await userService.GetSingleIdForUsernameAsync(username);
            await UpdateRolesNameForUserIdAsync(rolesName, userId);
        }

        public async Task UpdateRolesNameForUserIdAsync(IEnumerable<string> rolesName, Int64 userId)
        {
            var rolesId = await roleService.GetIdsForNamesAsync(rolesName);
            var existendRolesId = await GetRolesIdForUserIdAsync(userId);
            var noExistentRolesId = rolesId
                .Where(i => !existendRolesId.Contains(i))
                .ToList();

            foreach (var roleId in noExistentRolesId)
            {
                await CreateIfNotExistsAsync(new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }

            await DeleteAsync(new QueryOptions
            {
                Filters = {
                    { "userId", userId },
                    { Op.NotIn("roleId", rolesId) }
                }
            });
        }
    }
}
