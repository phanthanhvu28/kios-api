using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.TypeSale;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.TypeSale.Commands;
public class DeleteTypeSale : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteTypeSale, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly ITypeSaleRepository _typeSaleRepository;
        public Handler(IAppContextAccessor appContextAccessor, ITypeSaleRepository typeSaleRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _typeSaleRepository = typeSaleRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteTypeSale command, CancellationToken cancellationToken)
        {
            TypeSaleByCodeSpec typeSaleSpec = new(command.Code);
            Entities.TypeSales? typeSale = await _typeSaleRepository.FindOneAsync(typeSaleSpec);
            if (typeSale == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound type sale:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = typeSale.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _typeSaleRepository.UpdateAsync(typeSale);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete type sale:{command.Code} error"));
            }

            return ResultModel<string>.Create($"Delete type sale {command.Code} success");
        }
    }
}
