using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.Services.Common;
using ApplicationCore.Specifications.AuthenUser;
using ApplicationCore.UseCases.AuthenUser.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.AuthenUser.Commands;
public sealed class UpdateUserRole : UpdateUserRoleModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateRoleUserDto>
{
    public sealed class Handler : ICommandHandler<UpdateUserRole, ResultModel<UpdateRoleUserDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IAuthenUserRepository _authenRepository;
        private readonly IAuthenService _authenService;

        public Handler(IAppContextAccessor appContextAccessor,
            IAuthenUserRepository authenRepository,
            IAuthenService authenService)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _authenRepository = authenRepository;
            _authenService = authenService;

        }
        public async ValueTask<ResultModel<UpdateRoleUserDto>> Handle(UpdateUserRole command, CancellationToken cancellationToken)
        {
            if (_identityUser is null)
            {
                return ResultModel<UpdateRoleUserDto>.Create(new ValidationException(100036, $"Access deny, you have no permission to update role"));
            }

            AuthenUserByUsernameSpec authenSpec = new(command.Username);
            Entities.AuthenUser? user = await _authenRepository.FindOneAsync(authenSpec);
            if (user is null)
            {
                return ResultModel<UpdateRoleUserDto>.Create(new NotFoundException(100036, $"Notfound user:{command.Username} in system"));
            }

            user.UpdateBy = _identityUser!.FullName;
            user.UsernameEdit = _identityUser!.Username;
            user.Roles = command.Roles;

            bool result = await _authenRepository.UpdateAsync(user);
            if (!result)
            {
                return ResultModel<UpdateRoleUserDto>.Create(new ValidationException(100036, $"Update role for {command.Username} error"));
            }

            UpdateRoleUserDto dto = user.Adapt<UpdateRoleUserDto>();

            return ResultModel<UpdateRoleUserDto>.Create(dto);
        }
    }
}
