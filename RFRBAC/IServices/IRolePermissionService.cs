﻿using RFRBAC.Entities;
using RFService.IServices;
using RFService.Repo;

namespace RFRBAC.IServices
{
    public interface IRolePermissionService
        : IService<RolePermission>
    {
        Task<IEnumerable<Int64>> GetPermissionsIdForRolesIdAsync(IEnumerable<Int64> rolesId, GetOptions? options = null);
    }
}