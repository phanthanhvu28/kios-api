using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.AuthenUser;
using ApplicationCore.Services.Common;
using ApplicationCore.Specifications.AuthenUser;
using ApplicationCore.UseCases.AuthenUser.Models;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.AuthenUser.Commands;
public sealed class Login : UserModel, VELA.WebCoreBase.Core.Mediators.ICommand<LoginDto>
{
    public sealed class Handler : ICommandHandler<Login, ResultModel<LoginDto>>
    {
        private readonly IAuthenUserRepository _authenRepository;
        private readonly IAuthenService _authenService;
        public Handler(IAuthenUserRepository authenRepository,
            IAuthenService authenService)
        {
            _authenRepository = authenRepository;
            _authenService = authenService;
        }
        public async ValueTask<ResultModel<LoginDto>> Handle(Login command, CancellationToken cancellationToken)
        {
            string enCode = _authenService.Encrypt(command.Password);
            string deCode = _authenService.Decrypt(enCode);

            AuthenUserByUsernameSpec loginSpec = new(command.Username, enCode);
            Entities.AuthenUser? user = await _authenRepository.FindOneAsync(loginSpec);
            if (user == null)
            {
                return ResultModel<LoginDto>.Create(new ValidationException(100036, $"Not found {command.Username} in system"));
            }
            LoginDto result = new()
            {
                expires_in = 15,
                access_token = _authenService.GenerateToken(command),
                scope = "",
                id_token = "",
                token_type = "Bearer"
            };

            return ResultModel<LoginDto>.Create(result);
        }
    }
}
