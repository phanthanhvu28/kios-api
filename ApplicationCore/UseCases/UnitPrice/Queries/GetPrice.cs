using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.UnitPrice;
using ApplicationCore.Specifications.UnitPrice;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.UnitPrice.Queries;
public class GetPrice : IItemQuery<UnitPriceBaseDto>
{
    public string StoreCode { get; set; }
    public string ProductCode { get; set; }
    public sealed class Handler : IQueryHandler<GetPrice, ResultModel<UnitPriceBaseDto>>
    {
        private readonly ISetupPriceRepository _setupPriceRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            ISetupPriceRepository setupPriceRepository,
            IAppContextAccessor appContextAccessor)
        {

            _setupPriceRepository = setupPriceRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<UnitPriceBaseDto>> Handle(GetPrice query, CancellationToken cancellationToken)
        {
            UnitPriceByProductSpec spec = new(query.StoreCode, query.ProductCode);
            Entities.SetupPrice? unitPrice = await _setupPriceRepository.FindOneAsync(spec);
            UnitPriceBaseDto result = unitPrice.Adapt<UnitPriceBaseDto>();

            return ResultModel<UnitPriceBaseDto>.Create(result);
        }
    }
}
