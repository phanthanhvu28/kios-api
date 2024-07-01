﻿using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Store;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Store.Queries;
public class GetStore : PagingModel, IListQuery<StoreBaseDto>
{
    public sealed class Handler : IQueryHandler<GetStore, ListResultModel<StoreBaseDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(IStoreRepository storeRepository,
           IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<StoreBaseDto>> Handle(GetStore query, CancellationToken cancellationToken)
        {
            StoreByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Stores> entity = await _storeRepository.FindAsync(spec);
            long entityCount = await _storeRepository.CountAsync(spec);
            return ListResultModel<StoreBaseDto>.Create(
                 entity.Adapt<List<StoreBaseDto>>(), entityCount, query.Page, query.PageSize);
        }
    }
}
