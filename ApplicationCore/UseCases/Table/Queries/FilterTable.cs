using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Table;
using ApplicationCore.Specifications.Area;
using ApplicationCore.Specifications.Store;
using ApplicationCore.Specifications.TypeBida;
using ApplicationCore.Specifications.TypeSale;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Table.Queries;
public class FilterTable : IItemQuery<FilterTableDto>
{
    public sealed class Handler : IQueryHandler<FilterTable, ResultModel<FilterTableDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly ITypeBidaRepository _typeBidaRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            IAreaRepository areaRepository,
            ITypeSaleRepository typeSaleRepository,
            ITypeBidaRepository typeBidaRepository,
            IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _areaRepository = areaRepository;
            _typeSaleRepository = typeSaleRepository;
            _typeBidaRepository = typeBidaRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<FilterTableDto>> Handle(FilterTable query, CancellationToken cancellationToken)
        {
            FilterTableDto result = new();
            StoreGetFilterSpec spec = new();
            List<Entities.Stores> store = await _storeRepository.FindAsync(spec);

            AreaGetFilterSpec areaSpec = new();
            List<Entities.Areas> area = await _areaRepository.FindAsync(areaSpec);

            TypeSaleGetFilterSpec typeSaleSpec = new();
            List<Entities.TypeSales> typeSale = await _typeSaleRepository.FindAsync(typeSaleSpec);

            TypeBidaGetFilterSpec typeBidaSpec = new();
            List<Entities.TypeBida> typeBida = await _typeBidaRepository.FindAsync(typeBidaSpec);

            result.Store = store.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();
            result.Area = area.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();
            result.TypeSale = typeSale.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();
            result.TypeBida = typeBida.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();

            return ResultModel<FilterTableDto>.Create(result);
        }
    }
}
