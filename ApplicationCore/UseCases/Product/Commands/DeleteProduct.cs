using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Product;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Product.Commands;
public class DeleteProduct : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteProduct, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IProductRepository _productRepository;
        public Handler(IAppContextAccessor appContextAccessor,
            IProductRepository productRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _productRepository = productRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteProduct command, CancellationToken cancellationToken)
        {
            ProductByCodeSpec productSpec = new(command.Code);
            Entities.Products? product = await _productRepository.FindOneAsync(productSpec);
            if (product == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound product:[{command.Code}]"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = product.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _productRepository.UpdateAsync(product);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete product:[{product.Name}] error"));
            }

            return ResultModel<string>.Create($"Delete product [{product.Name}] success");
        }
    }
}
