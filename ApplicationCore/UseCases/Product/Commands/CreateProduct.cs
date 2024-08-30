using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Product;
using ApplicationCore.UseCases.Product.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Product.Commands;
public class CreateProduct : CreateProductModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreateProductDto>
{
    public sealed class Handler : ICommandHandler<CreateProduct, ResultModel<CreateProductDto>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IStaffRepository _staffRepository;
        private readonly IProductRepository _productRepository;
        public Handler(IAppContextAccessor appContextAccessor,
            IStaffRepository staffRepository,
            IProductRepository productRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _staffRepository = staffRepository;
            _productRepository = productRepository;
        }
        public async ValueTask<ResultModel<CreateProductDto>> Handle(CreateProduct command, CancellationToken cancellationToken)
        {
            Entities.Products @new = command.Adapt<Entities.Products>();
            @new.StaffCode = "";
            OneOf.OneOf<bool, CommonExceptionBase> process = @new.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<CreateProductDto>.Create(process.AsT1);
            }
            _ = await _productRepository.UpdateAsync(@new);

            return ResultModel<CreateProductDto>.Create(@new.Adapt<CreateProductDto>());
        }
    }
}
