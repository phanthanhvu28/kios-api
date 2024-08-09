using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.TypeSale;
using ApplicationCore.Specifications.Store;
using ApplicationCore.Specifications.TypeSale;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.TypeSale.Queries;
public class GetTypeSale : PagingModel, IListQuery<TypeSaleBaseDto>
{
    public sealed class Handler : IQueryHandler<GetTypeSale, ListResultModel<TypeSaleBaseDto>>
    {
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            ITypeSaleRepository typeSaleRepository,
            IAppContextAccessor appContextAccessor)
        {
            _typeSaleRepository = typeSaleRepository;
            _storeRepository = storeRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<TypeSaleBaseDto>> Handle(GetTypeSale query, CancellationToken cancellationToken)
        {
            TypeSaleByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.TypeSales> entity = await _typeSaleRepository.FindAsync(spec);
            List<TypeSaleBaseDto> result = entity.Adapt<List<TypeSaleBaseDto>>();

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

            long entityCount = await _typeSaleRepository.CountAsync(spec);
            return ListResultModel<TypeSaleBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
