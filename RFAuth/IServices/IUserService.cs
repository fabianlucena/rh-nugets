﻿using RFAuth.Entities;
using RFService.IServices;

namespace RFAuth.IServices
{
    public interface IUserService
        : IService<User>,
            IServiceId<User>,
            IServiceUuid<User>,
            IServiceIdUuid<User>,
            IServiceSoftDeleteUuid<User>,
            IServiceDecorated
    {
        Task<User> GetSingleForUsernameAsync(string username);

        Task<User?> GetSingleOrDefaultForUsernameAsync(string username);

        public async Task<Int64> GetSingleIdForUsernameAsync(string username)
        {
            var user = await GetSingleForUsernameAsync(username);
            return user.Id;
        }
    }
}
