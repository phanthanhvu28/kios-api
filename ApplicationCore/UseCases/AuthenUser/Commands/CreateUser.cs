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
public sealed class CreateUser : CreteUserModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateUserDto>
{
    public sealed class Handler : ICommandHandler<CreateUser, ResultModel<CreateUserDto>>
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
        public async ValueTask<ResultModel<CreateUserDto>> Handle(CreateUser command, CancellationToken cancellationToken)
        {
            if (_identityUser is null)
            {
                return ResultModel<CreateUserDto>.Create(new ValidationException(100036, $"Access deny, you have no permission to create user"));
            }

            AuthenUserByUsernameSpec authenSpec = new(command.Username);
            Entities.AuthenUser? user = await _authenRepository.FindOneAsync(authenSpec);
            if (user is not null)
            {
                return ResultModel<CreateUserDto>.Create(new ValidationException(100036, $"Exists {command.Username} in system"));
            }

            //string enCode = _authenService.Encrypt(command.Password);

            Entities.AuthenUser @new = command.Adapt<Entities.AuthenUser>();

            //@new.Password = enCode;

            @new.StoreCode = command.StoreCode;
            @new.CreateBy = _identityUser!.Username;
            @new.CreateBy = _identityUser!.FullName;

            bool result = await _authenRepository.UpdateAsync(@new);
            if (!result)
            {
                return ResultModel<CreateUserDto>.Create(new ValidationException(100036, $"Create {command.Username} error"));
            }

            CreateUserDto dto = command.Adapt<CreateUserDto>();

            return ResultModel<CreateUserDto>.Create(dto);
        }
    }
}
