﻿using RFAuth.Entities;
using Microsoft.Extensions.DependencyInjection;
using RFAuth.IServices;

namespace RFAuth
{
    public static class Setup
    {
        public static void ConfigureDataRFAuth(IServiceProvider provider)
            => ConfigureDataRFAuthAsync(provider).Wait();

        public static async Task ConfigureDataRFAuthAsync(IServiceProvider provider)
        {
            var userTypeService = provider.GetService<IUserTypeService>() ??
                throw new Exception("Can't get IUserTypeService.");

            var userType = await userTypeService.GetOrCreateAsync(new UserType
                {
                    Name = "user",
                    Title = "User",
                    IsTranslatable = true,
                });

            var userService = provider.GetService<IUserService>() ??
                throw new Exception("Can't get IUserService.");

            var user = await userService.GetOrCreateAsync(new User
                {
                    TypeId = userType.Id,
                    Username = "admin",
                    FullName = "Administrador",
                });

            var passwordService = provider.GetService<IPasswordService>() ??
                throw new Exception("Can't get IPasswordService.");

            await passwordService.CreateIfNotExistsAsync(new Password
            {
                UserId = user.Id,
                Hash = "$2a$11$fRe./FCGyNjS9Vao3IIBlOiVCx3C05NRBNFrHhVk32Qdw75Ia.Y5S",
            });

            var addRolePermissionService = provider.GetService<IAddRolePermissionService>();
            if (addRolePermissionService != null)
            {
                var rolesPermissions = new Dictionary<string, IEnumerable<string>>{
                    { "user",  [
                        "changePassword",
                    ] },

                    { "admin",  [
                        "changePassword",
                        "user.get", "user.add", "user.edit", "user.delete", "user.restore",
                    ] },
                };

                await addRolePermissionService.AddRolesPermissionsAsync(rolesPermissions);
            }
        }
    }
}
