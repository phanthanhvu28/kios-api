using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Staff;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Staff.Queries;
public class FilterStaff : IItemQuery<FilterStaffDto>
{
    public sealed class Handler : IQueryHandler<FilterStaff, ResultModel<FilterStaffDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(IStoreRepository storeRepository,
           IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<FilterStaffDto>> Handle(FilterStaff query, CancellationToken cancellationToken)
        {
            FilterStaffDto result = new();
            StoreGetFilterSpec spec = new();
            List<Entities.Stores> store = await _storeRepository.FindAsync(spec);

            result.Store = store.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();

            return ResultModel<FilterStaffDto>.Create(result);
        }
    }
}
