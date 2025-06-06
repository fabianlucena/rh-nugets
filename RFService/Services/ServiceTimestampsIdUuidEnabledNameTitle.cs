﻿using RFService.Entities;
using RFService.ILibs;
using RFService.IRepo;
using RFService.IServices;

namespace RFService.Services
{
    public abstract class ServiceTimestampsIdUuidEnabledNameTitle<TRepo, TEntity>(TRepo repo)
        : ServiceTimestampsIdUuidEnabledName<TRepo, TEntity>(repo),
            IServiceTitle<TEntity>
        where TRepo : IRepo<TEntity>
        where TEntity : EntityTimestampsIdUuidEnabledNameTitle
    {
        public override IDataDictionary SanitizeDataForAutoGet(IDataDictionary data)
            => base.SanitizeDataForAutoGet(
                ((IServiceTitle<TEntity>)this).SanitizeTitleForAutoGet(data)
            );
    }
}
