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
public sealed class UpdateMenu : UpdateMenuModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateMenuDto>
{
    public sealed class Handler : ICommandHandler<UpdateMenu, ResultModel<UpdateMenuDto>>
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
        public async ValueTask<ResultModel<UpdateMenuDto>> Handle(UpdateMenu command, CancellationToken cancellationToken)
        {
            if (_identityUser is null)
            {
                return ResultModel<UpdateMenuDto>.Create(new ValidationException(100036, $"Access deny, you have no permission to update menu"));
            }

            AuthenUserByUsernameSpec authenSpec = new(command.Username);
            Entities.AuthenUser? user = await _authenRepository.FindOneAsync(authenSpec);
            if (user is null)
            {
                return ResultModel<UpdateMenuDto>.Create(new NotFoundException(100036, $"Notfound user:{command.Username} in system"));
            }

            user.UpdateBy = _identityUser!.FullName;
            user.UsernameEdit = _identityUser!.Username;

            user.Menus = command.Menus;

            bool result = await _authenRepository.UpdateAsync(user);
            if (!result)
            {
                return ResultModel<UpdateMenuDto>.Create(new ValidationException(100036, $"Update menu for {command.Username} error"));
            }

            UpdateMenuDto dto = user.Adapt<UpdateMenuDto>();

            return ResultModel<UpdateMenuDto>.Create(dto);
        }
    }
}
