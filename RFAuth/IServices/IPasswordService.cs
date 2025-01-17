﻿using RFAuth.Entities;
using RFService.ILibs;
using RFService.IServices;
using RFService.Repo;

namespace RFAuth.IServices
{
    public interface IPasswordService
        : IServiceId<Password>
    {
        Task<Password> GetSingleForUserIdAsync(Int64 userId);

        Task<Password?> GetSingleOrDefaultForUserIdAsync(Int64 userId);

        Task<Password> GetSingleForUserAsync(User user);

        Task<Password?> GetSingleOrDefaultForUserAsync(User user);

        string Hash(string password);

        bool Verify(string rawPassword, string hash);

        bool Verify(string rawPassword, Password password);

        Task<int> UpdateForUserIdAsync(Int64 userId, IDataDictionary data, GetOptions? options = null);

        Task<bool> CreateOrUpdateForUserIdAsync(Int64 userId, string password);

        Task<bool> CreateOrUpdateForUsernameAsync(string username, string password);
    }
}
