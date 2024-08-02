using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Staff;
using ApplicationCore.Specifications.Staff;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Staff.Queries;
public class GetStaff : PagingModel, IListQuery<StaffBaseDto>
{
    public sealed class Handler : IQueryHandler<GetStaff, ListResultModel<StaffBaseDto>>
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStaffRepository staffRepository,
            IStoreRepository storeRepository,
            IAppContextAccessor appContextAccessor)
        {
            _staffRepository = staffRepository;
            _storeRepository = storeRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<StaffBaseDto>> Handle(GetStaff query, CancellationToken cancellationToken)
        {
            StaffByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Staffs> entity = await _staffRepository.FindAsync(spec);
            List<StaffBaseDto> result = entity.Adapt<List<StaffBaseDto>>();

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

            long entityCount = await _staffRepository.CountAsync(spec);
            return ListResultModel<StaffBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
