using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Role;
using ApplicationCore.Services.Common;
using ApplicationCore.UseCases.Role.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Role.Commands;
public class CreateRole : CreateRoleModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateRoleDto>
{
    public sealed class Handler : ICommandHandler<CreateRole, ResultModel<CreateRoleDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IAuthenUserRepository _authenRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthenService _authenService;

        public Handler(IAppContextAccessor appContextAccessor,
            IAuthenUserRepository authenRepository,
            IRoleRepository roleRepository,
            IAuthenService authenService)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _authenRepository = authenRepository;
            _roleRepository = roleRepository;
            _authenService = authenService;

        }
        public async ValueTask<ResultModel<CreateRoleDto>> Handle(CreateRole command, CancellationToken cancellationToken)
        {
            Entities.Role @new = command.Adapt<Entities.Role>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateRoleDto>.Create(process.AsT1);
            }
            bool result = await _roleRepository.UpdateAsync(@new);
            if (!result)
            {
                return ResultModel<CreateRoleDto>.Create(new NotFoundException(100036, $"Update role:{command.Name} error"));
            }

            return ResultModel<CreateRoleDto>.Create(@new.Adapt<CreateRoleDto>());
        }
    }
}
