using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Store;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Store.Commands;
public class DeleteStore : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteStore, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStoreRepository _storeRepository;
        public Handler(IAppContextAccessor appContextAccessor, IStoreRepository storeRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _storeRepository = storeRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteStore command, CancellationToken cancellationToken)
        {
            StoreByCodeSpec companySpec = new(command.Code);
            Entities.Stores? store = await _storeRepository.FindOneAsync(companySpec);
            if (store == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound store:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = store.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _storeRepository.UpdateAsync(store);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete store:{command.Code} error"));
            }

            return ResultModel<string>.Create($"Delete store {command.Code} success");
        }
    }
}
