using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.TypeSale;
using ApplicationCore.UseCases.TypeSale.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.TypeSale.Commands;
public class CreateTypeSale : CreateTypeSaleModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateTypeSaleDto>
{
    public sealed class Handler : ICommandHandler<CreateTypeSale, ResultModel<CreateTypeSaleDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITypeSaleRepository _typeSaleRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITypeSaleRepository typeSaleRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _typeSaleRepository = typeSaleRepository;
        }
        public async ValueTask<ResultModel<CreateTypeSaleDto>> Handle(CreateTypeSale command, CancellationToken cancellationToken)
        {
            Entities.TypeSales @new = command.Adapt<Entities.TypeSales>();
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateTypeSaleDto>.Create(process.AsT1);
            }
            _ = await _typeSaleRepository.UpdateAsync(@new);

            return ResultModel<CreateTypeSaleDto>.Create(@new.Adapt<CreateTypeSaleDto>());
        }
    }
}
