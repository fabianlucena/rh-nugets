﻿using RFService.Entities;
using RFService.Exceptions;
using RFService.Repo;

namespace RFService.IServices
{
    public interface IServiceIdUuidName<TEntity>
        : IServiceName<TEntity>,
            IServiceId<TEntity>
        where TEntity : Entity
    {
        public async Task<Int64> GetSingleIdForNameAsync(string name, QueryOptions? options = null, Func<string, TEntity>? creator = null)
        {
            var item = await GetSingleOrDefaultForNameAsync(name, options);
            if (item == null)
            {
                if (creator != null)
                    item = creator(name);

                if (item == null)
                    throw new NamedItemNotFoundException(name);

                await CreateAsync(item);
            }

            return GetId(item);
        }

        public async Task<Int64?> GetSingleOrDefaultIdForNameAsync(string name, QueryOptions? options = null)
        {
            var item = await GetSingleOrDefaultForNameAsync(name, options);
            if (item == null)
                return null;

            return GetId(item);
        }

        public async Task<Int64> GetSingleIdForNameOrCreateAsync(string name, TEntity data, QueryOptions? options = null)
        {
            var item = await GetSingleOrDefaultForNameAsync(name, options);
            item ??= await CreateAsync(data, options);

            return GetId(item);
        }

        public async Task<Int64> GetSingleIdForNameOrCreateAsync(string name, Func<TEntity> dataFactory, QueryOptions? options = null)
        {
            var item = await GetSingleOrDefaultForNameAsync(name, options);
            item ??= await CreateAsync(dataFactory(), options);

            return GetId(item);
        }

        public async Task<IEnumerable<TEntity>> GetListForNamesAsync(IEnumerable<string> names, QueryOptions? options = null)
        {
            options ??= new QueryOptions();
            options.AddFilter("Name", names);

            return await GetListAsync(options);
        }

        public async Task<IEnumerable<Int64>> GetIdsForNamesAsync(IEnumerable<string> names, QueryOptions? options = null)
            => (await GetListForNamesAsync(names, options)).Select(GetId);
    }
}
