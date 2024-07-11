using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Store;
using ApplicationCore.Specifications.Store;
using ApplicationCore.UseCases.Store.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Store.Commands;
public class UpdateStore : UpdateStoreModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateStoreDto>
{
    public sealed class Handler : ICommandHandler<UpdateStore, ResultModel<UpdateStoreDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStoreRepository _storeRepository;
        public Handler(IAppContextAccessor appContextAccessor, IStoreRepository storeRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _storeRepository = storeRepository;
        }
        public async ValueTask<ResultModel<UpdateStoreDto>> Handle(UpdateStore command, CancellationToken cancellationToken)
        {
            StoreByCodeSpec companySpec = new(command.Code);
            Entities.Stores? store = await _storeRepository.FindOneAsync(companySpec);
            if (store == null)
            {
                return ResultModel<UpdateStoreDto>.Create(new NotFoundException(100036, $"Notfound store:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = store.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateStoreDto>.Create(process.AsT1);
            }

            store.Name = command.Name;
            store.Address = command.Address;
            store.Email = command.Email;
            store.Phone = command.Phone;
            store.CompanyCode = command.CompanyCode;

            bool result = await _storeRepository.UpdateAsync(store);
            if (!result)
            {
                return ResultModel<UpdateStoreDto>.Create(new NotFoundException(100036, $"Update store:{command.Code} error"));
            }

            return ResultModel<UpdateStoreDto>.Create(store.Adapt<UpdateStoreDto>());
        }
    }
}
