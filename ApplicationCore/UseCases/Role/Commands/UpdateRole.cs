using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Role;
using ApplicationCore.Specifications.Role;
using ApplicationCore.UseCases.Role.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Role.Commands;
public class UpdateRole : UpdateRoleModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateRoleDto>
{
    public sealed class Handler : ICommandHandler<UpdateRole, ResultModel<UpdateRoleDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStaffRepository _staffRepository;
        private readonly IRoleRepository _roleRepository;
        public Handler(IAppContextAccessor appContextAccessor,
            IStaffRepository staffRepository,
            IRoleRepository roleRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _staffRepository = staffRepository;
            _roleRepository = roleRepository;
        }
        public async ValueTask<ResultModel<UpdateRoleDto>> Handle(UpdateRole command, CancellationToken cancellationToken)
        {
            RoleByCodeSpec roleSpec = new(command.Code);
            Entities.Role? role = await _roleRepository.FindOneAsync(roleSpec);
            if (role == null)
            {
                return ResultModel<UpdateRoleDto>.Create(new NotFoundException(100036, $"Notfound role:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = role.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateRoleDto>.Create(process.AsT1);
            }

            role.Name = command.Name;
            role.Menus = command.Menus;

            bool result = await _roleRepository.UpdateAsync(role);
            if (!result)
            {
                return ResultModel<UpdateRoleDto>.Create(new NotFoundException(100036, $"Update role:{command.Code} error"));
            }

            return ResultModel<UpdateRoleDto>.Create(role.Adapt<UpdateRoleDto>());
        }
    }
}
