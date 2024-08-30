using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Product;
using ApplicationCore.Specifications.Product;
using ApplicationCore.UseCases.Product.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Product.Commands;
public class UpdateProduct : UpdateProductModel, VELA.WebCoreBase.Core.Mediators.ICommand<UpdateProductDto>
{
    public sealed class Handler : ICommandHandler<UpdateProduct, ResultModel<UpdateProductDto>>
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
        public async ValueTask<ResultModel<UpdateProductDto>> Handle(UpdateProduct command, CancellationToken cancellationToken)
        {
            ProductByCodeSpec productSpec = new(command.Code);
            Entities.Products? product = await _productRepository.FindOneAsync(productSpec);
            if (product == null)
            {
                return ResultModel<UpdateProductDto>.Create(new NotFoundException(100036, $"Notfound product:{command.Code}"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = product.ProcessStep(new UpdateProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<UpdateProductDto>.Create(process.AsT1);
            }

            product.Name = command.Name;
            product.TypeBidaCode = command.TypeBidaCode;
            product.TypeSaleCode = command.TypeSaleCode;
            product.StoreCode = command.StoreCode;

            bool result = await _productRepository.UpdateAsync(product);
            if (!result)
            {
                return ResultModel<UpdateProductDto>.Create(new NotFoundException(100036, $"Update product:{command.Code} error"));
            }

            return ResultModel<UpdateProductDto>.Create(product.Adapt<UpdateProductDto>());
        }
    }
}
