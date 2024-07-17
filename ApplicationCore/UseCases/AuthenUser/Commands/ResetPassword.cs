using ApplicationCore.Contracts.RepositoryBase;
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
public sealed class ResetPassword : ResetPasswordModel, VELA.WebCoreBase.Core.Mediators.ICommand<bool>
{
    public sealed class Handler : ICommandHandler<ResetPassword, ResultModel<bool>>
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
        public async ValueTask<ResultModel<bool>> Handle(ResetPassword command, CancellationToken cancellationToken)
        {
            if (_identityUser is null)
            {
                return ResultModel<bool>.Create(new ValidationException(100036, $"Access deny, you have no permission to reset password"));
            }

            AuthenUserByUsernameSpec authenSpec = new(command.Username);
            Entities.AuthenUser? user = await _authenRepository.FindOneAsync(authenSpec);
            if (user is null)
            {
                return ResultModel<bool>.Create(new ValidationException(100036, $"Notfound {command.Username} in system"));
            }
            if (command.Password != command.ConfirmedPass)
            {
                return ResultModel<bool>.Create(new ValidationException(100036, $"Please input again confirmed password"));
            }

            string enCode = _authenService.Encrypt(command.Password);

            user.Password = enCode;

            user.UpdateBy = _identityUser!.FullName;
            user.UsernameEdit = _identityUser!.Username;

            bool result = await _authenRepository.UpdateAsync(user);
            if (!result)
            {
                return ResultModel<bool>.Create(new ValidationException(100036, $"Reset password {command.Username} error"));
            }
            return ResultModel<bool>.Create(true);
        }
    }
}
