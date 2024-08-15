using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Role;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Role.Commands;
public class DeleteRole : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteRole, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IRoleRepository _roleRepository;
        public Handler(IAppContextAccessor appContextAccessor,
            IRoleRepository roleRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _roleRepository = roleRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteRole command, CancellationToken cancellationToken)
        {
            RoleByCodeSpec roleSpec = new(command.Code);
            Entities.Role? role = await _roleRepository.FindOneAsync(roleSpec);
            if (role == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound role:[{command.Code}]"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = role.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _roleRepository.UpdateAsync(role);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete role:[{role.Name}] error"));
            }

            return ResultModel<string>.Create($"Delete role [{role.Name}] success");
        }
    }
}
