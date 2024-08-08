using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Table;
using ApplicationCore.Specifications.Area;
using ApplicationCore.Specifications.Store;
using ApplicationCore.Specifications.Table;
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
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            ITableRepository tableRepository,
            IAreaRepository areaRepository,
            IStoreRepository storeRepository,
            IAppContextAccessor appContextAccessor)
        {
            _tableRepository = tableRepository;
            _areaRepository = areaRepository;
            _storeRepository = storeRepository;
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

            long entityCount = await _tableRepository.CountAsync(spec);
            return ListResultModel<TableBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
