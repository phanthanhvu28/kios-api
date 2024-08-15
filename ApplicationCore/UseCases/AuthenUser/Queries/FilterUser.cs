using ApplicationCore.Constants;
using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.Specifications.Role;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.AuthenUser.Queries;
public class FilterUser : IItemQuery<FilterUserDto>
{
    public sealed class Handler : IQueryHandler<FilterUser, ResultModel<FilterUserDto>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(IStoreRepository storeRepository,
            IRoleRepository roleRepository,
            IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _roleRepository = roleRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<FilterUserDto>> Handle(FilterUser query, CancellationToken cancellationToken)
        {
            FilterUserDto result = new();
            StoreGetFilterSpec spec = new();
            List<Entities.Stores> store = await _storeRepository.FindAsync(spec);

            RoleGetFilterSpec roleSpec = new();
            List<Entities.Role> roles = await _roleRepository.FindAsync(roleSpec);
            if (roles != null && roles.Count > 0)
            {
                result.Roles = roles.Select(e => new RoleDto { Code = e.Code, Name = e.Name }).ToList();
            }
            result.Store = store.Select(e => new ValueFilterObject { Value = new { e.Code, e.Name }, Label = e.Name }).ToList();
            result.Menus = AuthenSite.Site._menus;

            return ResultModel<FilterUserDto>.Create(result);
        }
    }
}
