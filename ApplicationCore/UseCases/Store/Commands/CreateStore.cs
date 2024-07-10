using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Store;
using ApplicationCore.UseCases.Store.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Store.Commands;
public sealed class CreateStore : CreateStoreModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateStoreDto>
{
    public sealed class Handler : ICommandHandler<CreateStore, ResultModel<CreateStoreDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStoreRepository _storeRepository;
        public Handler(IAppContextAccessor appContextAccessor, IStoreRepository storeRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _storeRepository = storeRepository;
        }
        public async ValueTask<ResultModel<CreateStoreDto>> Handle(CreateStore command, CancellationToken cancellationToken)
        {
            Entities.Stores @new = command.Adapt<Entities.Stores>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateStoreDto>.Create(process.AsT1);
            }
            _ = await _storeRepository.UpdateAsync(@new);

            return ResultModel<CreateStoreDto>.Create(@new.Adapt<CreateStoreDto>());
        }
    }
}
