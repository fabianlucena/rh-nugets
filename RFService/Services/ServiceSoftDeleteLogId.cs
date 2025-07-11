﻿using RFService.Entities;
using RFService.Exceptions;
using RFService.ILibs;
using RFService.IRepo;
using RFService.IServices;
using RFService.Repo;
using System.Reflection;

namespace RFService.Services
{
    public abstract class ServiceSoftDeleteLogId<TRepo, TEntity>(TRepo repo)
        : ServiceSoftDelete<TRepo, TEntity>(repo),
            IServiceId<TEntity>
        where TRepo : IRepo<TEntity>
        where TEntity : EntitySoftDeleteLogId
    {
        public Int64 GetId(TEntity item) => item.Id;

        public override QueryOptions SanitizeQueryOptions(QueryOptions options)
        {
            if (!options.HasColumnFilter("UpdatedOfId"))
            {
                options = new QueryOptions(options);
                options.AddFilter("UpdatedOfId", null);
            }

            return base.SanitizeQueryOptions(options);
        }

        public async Task<IEnumerable<Int64>> GetListIdAsync(QueryOptions options)
            => (await GetListAsync(options)).Select(GetId);

        public override async Task<TEntity> ValidateForCreationAsync(TEntity data)
        {
            data = await base.ValidateForCreationAsync(data);

            if (data.Id != 0)
                throw new ForbiddenIdForCreationException();

            if (data.UpdatedAt == default)
                data.UpdatedAt = DateTime.UtcNow;

            return data;
        }

        public override IDataDictionary SanitizeDataForAutoGet(IDataDictionary data)
            => base.SanitizeDataForAutoGet(
                ((IServiceLogId<TEntity>)this).SanitizeLogIdForAutoGet(data)
            );

        public virtual Task<TEntity> GetSingleForIdAsync(Int64 id, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("Id", id);
            return GetSingleAsync(options);
        }

        public async virtual Task<Int64> GetSingleIdAsync(QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            var item = await GetSingleAsync(options);
            return GetId(item);
        }

        public virtual Task<TEntity?> GetSingleOrDefaultForIdAsync(Int64 id, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("Id", id);
            return GetSingleOrDefaultAsync(options);
        }

        public virtual Task<IEnumerable<TEntity>> GetListForIdsAsync(IEnumerable<Int64> id, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("Id", id);
            return GetListAsync(options);
        }

        public override async Task<IDataDictionary> ValidateForUpdateAsync(IDataDictionary data, QueryOptions options)
        {
            data = await base.ValidateForUpdateAsync(data, options);

            if (!data.ContainsKey("UpdatedAt"))
                data["UpdatedAt"] = DateTime.UtcNow;

            return data;
        }

        public override async Task<int> UpdateAsync(IDataDictionary data, QueryOptions options)
        {
            data = await ValidateForUpdateAsync(data, options);

            var type = typeof(TEntity);
            var uuidProp = type.GetProperty("Uuid", BindingFlags.Public | BindingFlags.Instance);
            if (uuidProp != null && (!uuidProp.CanWrite || uuidProp.PropertyType != typeof(Guid)))
                uuidProp = null;

            var listOptions = new QueryOptions(options) { Switches = { { "IncludeDisabled", true } } };
            if (data.TryGetValue("DeletedAt", out object? value) && value == null)
                listOptions.Switches["IncludeDeleted"] = true;

            var result = 0;
            var list = await GetListAsync(listOptions);
            foreach (var item in list)
            {
                item.UpdatedOfId = item.Id;
                item.Id = default;
                uuidProp?.SetValue(item, Guid.NewGuid());

                await repo.InsertAsync(item, options.RepoOptions);

                data["UpdatedOfId"] = null;
                result += await repo.UpdateAsync(data, options);
            }


            return result;
        }

        public virtual Task<int> UpdateForIdAsync(IDataDictionary data, Int64 id, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("Id", id);
            return UpdateAsync(data, options);
        }

        public virtual Task<int> DeleteForIdAsync(Int64 id, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("Id", id);
            return DeleteAsync(options);
        }
    }
}