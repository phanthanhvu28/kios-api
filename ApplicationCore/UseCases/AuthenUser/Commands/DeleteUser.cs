using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Specifications.AuthenUser;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.AuthenUser.Commands;
public class DeleteUser : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Username { get; set; }
    public sealed class Handler : ICommandHandler<DeleteUser, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IAuthenUserRepository _userRepository;
        public Handler(IAppContextAccessor appContextAccessor, IAuthenUserRepository userRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _userRepository = userRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteUser command, CancellationToken cancellationToken)
        {
            AuthenUserByUsernameSpec userSpec = new(command.Username);
            Entities.AuthenUser? user = await _userRepository.FindOneAsync(userSpec);
            if (user == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound user:{command.Username}"));
            }

            user.IsDelete = true;
            user.UsernameEdit = _identityUser!.Username;
            user.UpdateBy = _identityUser!.FullName;

            bool result = await _userRepository.UpdateAsync(user);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete user:{command.Username} error"));
            }

            return ResultModel<string>.Create($"Delete user {command.Username} success");
        }
    }
}
