using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.TypeBida;
using ApplicationCore.Specifications.Store;
using ApplicationCore.Specifications.TypeBida;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.TypeBida.Queries;
public class GetTypeBida : PagingModel, IListQuery<TypeBidaBaseDto>
{
    public sealed class Handler : IQueryHandler<GetTypeBida, ListResultModel<TypeBidaBaseDto>>
    {
        private readonly ITypeBidaRepository _typeBidaRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            ITypeBidaRepository typeBidaRepository,
            IAppContextAccessor appContextAccessor)
        {
            _typeBidaRepository = typeBidaRepository;
            _storeRepository = storeRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<TypeBidaBaseDto>> Handle(GetTypeBida query, CancellationToken cancellationToken)
        {
            TypeBidaByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.TypeBida> entity = await _typeBidaRepository.FindAsync(spec);
            List<TypeBidaBaseDto> result = entity.Adapt<List<TypeBidaBaseDto>>();

            StoreByArrayCodeSpec storeSpec = new(entity.Select(e => e.StoreCode).ToArray());
            List<Entities.Stores> stores = await _storeRepository.FindAsync(storeSpec);

            Dictionary<string, string> storeDictionary = stores.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(storeDto =>
            {
                if (storeDictionary.TryGetValue(storeDto.StoreCode, out string? storeName))
                {
                    storeDto.StoreName = storeName;
                }
                else
                {
                    storeDto.StoreName = "";
                }
            });

            long entityCount = await _typeBidaRepository.CountAsync(spec);
            return ListResultModel<TypeBidaBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
