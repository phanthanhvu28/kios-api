using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Role;
using ApplicationCore.Specifications.Role;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Role.Queries;
public class GetRole : PagingModel, IListQuery<RoleBaseDto>
{
    public sealed class Handler : IQueryHandler<GetRole, ListResultModel<RoleBaseDto>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IRoleRepository roleRepository,
            IAppContextAccessor appContextAccessor)
        {
            _roleRepository = roleRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ListResultModel<RoleBaseDto>> Handle(GetRole query, CancellationToken cancellationToken)
        {
            RoleByFilterSpec spec = new(query.Filters, query.Sorts, query.Page, query.PageSize);
            List<Entities.Role> entity = await _roleRepository.FindAsync(spec);
            List<RoleBaseDto> result = entity.Adapt<List<RoleBaseDto>>();

            long entityCount = await _roleRepository.CountAsync(spec);
            return ListResultModel<RoleBaseDto>.Create(result, entityCount, query.Page, query.PageSize);
        }
    }
}
