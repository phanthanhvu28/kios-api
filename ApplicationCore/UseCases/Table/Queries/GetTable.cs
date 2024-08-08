using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Table;
using ApplicationCore.Specifications.Area;
using ApplicationCore.Specifications.Store;
using ApplicationCore.Specifications.Table;
using ApplicationCore.Specifications.TypeBida;
using ApplicationCore.Specifications.TypeSale;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Table.Queries;
public class GetTable : PagingModel, IListQuery<TableBaseDto>
{
    public sealed class Handler : IQueryHandler<GetTable, ListResultModel<TableBaseDto>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly ITypeBidaRepository _typeBidaRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            ITableRepository tableRepository,
            IAreaRepository areaRepository,
            IStoreRepository storeRepository,
            ITypeSaleRepository typeSaleRepository,
            ITypeBidaRepository typeBidaRepository,
            IAppContextAccessor appContextAccessor)
        {
            _tableRepository = tableRepository;
            _areaRepository = areaRepository;
            _storeRepository = storeRepository;
            _typeSaleRepository = typeSaleRepository;
            _typeBidaRepository = typeBidaRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<TableBaseDto>> Handle(GetTable query, CancellationToken cancellationToken)
        {
            TableByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Tables> entity = await _tableRepository.FindAsync(spec);
            List<TableBaseDto> result = entity.Adapt<List<TableBaseDto>>();

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

            AreaByArrayCodeSpec areaSpec = new(entity.Select(e => e.AreaCode).ToArray());
            List<Entities.Areas> area = await _areaRepository.FindAsync(areaSpec);

            Dictionary<string, string> areaDictionary = area.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(areaDto =>
            {
                if (areaDictionary.TryGetValue(areaDto.AreaCode, out string? areaName))
                {
                    areaDto.AreaName = areaName;
                }
                else
                {
                    areaDto.AreaName = "";
                }
            });

            TypeBidaByArrayCodeSpec typeBidaSpec = new(entity.Select(e => e.TypeBidaCode).ToArray());
            List<Entities.TypeBida> typeBida = await _typeBidaRepository.FindAsync(typeBidaSpec);

            Dictionary<string, string> typeBidaDictionary = typeBida.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(typeBidaDto =>
            {
                if (typeBidaDictionary.TryGetValue(typeBidaDto.TypeBidaCode, out string? typeBidaName))
                {
                    typeBidaDto.TypeBidaName = typeBidaName;
                }
                else
                {
                    typeBidaDto.TypeBidaName = "";
                }
            });

            TypeSaleByArrayCodeSpec typeSaleSpec = new(entity.Select(e => e.TypeSaleCode).ToArray());
            List<Entities.TypeSales> typeSale = await _typeSaleRepository.FindAsync(typeSaleSpec);

            Dictionary<string, string> typeSaleDictionary = typeSale.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(typeSaleDto =>
            {
                if (typeSaleDictionary.TryGetValue(typeSaleDto.TypeSaleCode, out string? typeSaleName))
                {
                    typeSaleDto.TypeSaleName = typeSaleName;
                }
                else
                {
                    typeSaleDto.TypeSaleName = "";
                }
            });

            long entityCount = await _tableRepository.CountAsync(spec);
            return ListResultModel<TableBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
