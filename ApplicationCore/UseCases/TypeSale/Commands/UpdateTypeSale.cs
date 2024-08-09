using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.TypeSale;
using ApplicationCore.Specifications.TypeSale;
using ApplicationCore.UseCases.TypeSale.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.TypeSale.Commands;
public class UpdateTypeSale : UpdateTypeSaleModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateTypeSaleDto>
{
    public sealed class Handler : ICommandHandler<UpdateTypeSale, ResultModel<UpdateTypeSaleDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITypeSaleRepository _typeSaleRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITypeSaleRepository typeSaleRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _typeSaleRepository = typeSaleRepository;
        }
        public async ValueTask<ResultModel<UpdateTypeSaleDto>> Handle(UpdateTypeSale command, CancellationToken cancellationToken)
        {
            TypeSaleByCodeSpec typeSaleSpec = new(command.Code);
            Entities.TypeSales? typeSale = await _typeSaleRepository.FindOneAsync(typeSaleSpec);
            if (typeSale == null)
            {
                return ResultModel<UpdateTypeSaleDto>.Create(new NotFoundException(100036, $"Notfound type sale:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = typeSale.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateTypeSaleDto>.Create(process.AsT1);
            }

            typeSale.Name = command.Name;
            typeSale.StoreCode = command.StoreCode;
            typeSale.StaffCode = command.StaffCode;

            bool result = await _typeSaleRepository.UpdateAsync(typeSale);
            if (!result)
            {
                return ResultModel<UpdateTypeSaleDto>.Create(new NotFoundException(100036, $"Update type sale:{command.Code} error"));
            }

            return ResultModel<UpdateTypeSaleDto>.Create(typeSale.Adapt<UpdateTypeSaleDto>());
        }
    }
}
