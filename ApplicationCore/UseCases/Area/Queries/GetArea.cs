using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Area;
using ApplicationCore.Specifications.Area;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Area.Queries;
public class GetArea : PagingModel, IListQuery<AreaBaseDto>
{
    public sealed class Handler : IQueryHandler<GetArea, ListResultModel<AreaBaseDto>>
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IAreaRepository areaRepository,
            IStoreRepository storeRepository,
            IAppContextAccessor appContextAccessor)
        {
            _areaRepository = areaRepository;
            _storeRepository = storeRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<AreaBaseDto>> Handle(GetArea query, CancellationToken cancellationToken)
        {
            AreaByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Areas> entity = await _areaRepository.FindAsync(spec);
            List<AreaBaseDto> result = entity.Adapt<List<AreaBaseDto>>();

            StoreByArrayCodeSpec storeSpec = new(entity.Select(e => e.StoreCode).ToArray());
            List<Entities.Stores> stores = await _storeRepository.FindAsync(storeSpec);

            Dictionary<string, string> companyDictionary = stores.ToDictionary(c => c.Code, c => c.Name);

            result.ForEach(storeDto =>
            {
                if (companyDictionary.TryGetValue(storeDto.StoreCode, out string? companyName))
                {
                    storeDto.StoreName = companyName;
                }
                else
                {
                    storeDto.StoreName = "";
                }
            });

            long entityCount = await _areaRepository.CountAsync(spec);
            return ListResultModel<AreaBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
