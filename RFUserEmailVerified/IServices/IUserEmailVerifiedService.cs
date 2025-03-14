﻿using RFService.IServices;
using RFService.Repo;
using RFUserEmailVerified.Entities;

namespace RFUserEmailVerified.IServices
{
    public interface IUserEmailVerifiedService
        : IServiceId<UserEmailVerified>
    {
        Task<UserEmailVerified?> GetSingleOrDefaultForUserIdAsync(long userId, GetOptions? options = null);

        Task SetIsVerifiedForIdAsync(bool isVerified, long id);
    }
}
