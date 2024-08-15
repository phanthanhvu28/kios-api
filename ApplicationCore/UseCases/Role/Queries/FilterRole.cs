using ApplicationCore.Constants;
using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Role;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Role.Queries;
public class FilterRole : IItemQuery<FilterRoleDto>
{
    public sealed class Handler : IQueryHandler<FilterRole, ResultModel<FilterRoleDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(IStoreRepository storeRepository,
           IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<FilterRoleDto>> Handle(FilterRole query, CancellationToken cancellationToken)
        {
            FilterRoleDto result = new();
            StoreGetFilterSpec spec = new();
            List<Entities.Stores> store = await _storeRepository.FindAsync(spec);

            result.Menus = AuthenSite.Site._menus;

            return ResultModel<FilterRoleDto>.Create(result);
        }
    }
}
