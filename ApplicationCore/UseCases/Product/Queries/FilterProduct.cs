using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Product;
using ApplicationCore.Specifications.Store;
using ApplicationCore.Specifications.TypeBida;
using ApplicationCore.Specifications.TypeSale;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Product.Queries;
public class FilterProduct : IItemQuery<FilterProductDto>
{
    public sealed class Handler : IQueryHandler<FilterProduct, ResultModel<FilterProductDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly ITypeBidaRepository _typeBidaRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            ITypeSaleRepository typeSaleRepository,
            ITypeBidaRepository typeBidaRepository,
            IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _typeSaleRepository = typeSaleRepository;
            _typeBidaRepository = typeBidaRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<FilterProductDto>> Handle(FilterProduct query, CancellationToken cancellationToken)
        {
            FilterProductDto result = new();
            StoreGetFilterSpec spec = new();
            List<Entities.Stores> store = await _storeRepository.FindAsync(spec);

            TypeSaleGetFilterSpec typeSaleSpec = new();
            List<Entities.TypeSales> typeSale = await _typeSaleRepository.FindAsync(typeSaleSpec);

            TypeBidaGetFilterSpec typeBidaSpec = new();
            List<Entities.TypeBida> typeBida = await _typeBidaRepository.FindAsync(typeBidaSpec);

            result.Store = store.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();
            result.TypeSale = typeSale.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();
            result.TypeBida = typeBida.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();

            return ResultModel<FilterProductDto>.Create(result);
        }
    }
}
