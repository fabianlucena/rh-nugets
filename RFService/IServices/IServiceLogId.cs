﻿using RFService.ILibs;
using RFService.Libs;
using RFService.Repo;

namespace RFService.IServices
{
    public interface IServiceLogId<TEntity>
        : IService<TEntity>
        where TEntity : class
    {
        public IDataDictionary SanitizeLogIdForAutoGet(IDataDictionary data)
        {
            if (data.TryGetValue("UpdatedOfId", out object? updatedOfId))
            {
                if (updatedOfId == null
                    || (Int64)updatedOfId == default
                )
                {
                    data = new DataDictionary(data);
                    data.Remove("UpdatedOfId");
                }
            }

            return data;
        }

        Task<int> RestoreAsync(QueryOptions options)
            => UpdateAsync(new DataDictionary { { "Id" , null } }, options);

        Task<int> RestoreForIdAsync(Int64 id, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("Id", id);
            return RestoreAsync(options);
        }
    }
}
