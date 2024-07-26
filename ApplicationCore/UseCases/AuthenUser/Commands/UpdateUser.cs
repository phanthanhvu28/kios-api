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
public sealed class UpdateUser : UpdateUserModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateUserDto>
{
    public sealed class Handler : ICommandHandler<UpdateUser, ResultModel<UpdateUserDto>>
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
        public async ValueTask<ResultModel<UpdateUserDto>> Handle(UpdateUser command, CancellationToken cancellationToken)
        {
            if (_identityUser is null)
            {
                return ResultModel<UpdateUserDto>.Create(new ValidationException(100036, $"Access deny, you have no permission to update user"));
            }

            AuthenUserByUsernameSpec authenSpec = new(command.Username);
            Entities.AuthenUser? user = await _authenRepository.FindOneAsync(authenSpec);
            if (user is null)
            {
                return ResultModel<UpdateUserDto>.Create(new NotFoundException(100036, $"Notfound user:{command.Username} in system"));
            }
            user.Fullname = command.Fullname;
            user.Email = command.Email;
            user.Address = command.Address;
            user.Phone = command.Phone;

            user.UpdateBy = _identityUser!.FullName;
            user.UsernameEdit = _identityUser!.Username;

            user.Menus = command.Menus;

            bool result = await _authenRepository.UpdateAsync(user);
            if (!result)
            {
                return ResultModel<UpdateUserDto>.Create(new ValidationException(100036, $"Create {command.Username} error"));
            }

            UpdateUserDto dto = user.Adapt<UpdateUserDto>();

            return ResultModel<UpdateUserDto>.Create(dto);
        }
    }
}
