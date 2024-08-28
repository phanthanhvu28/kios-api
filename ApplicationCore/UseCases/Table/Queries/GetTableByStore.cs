using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Table;
using ApplicationCore.Specifications.Table;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Table.Queries;
public class GetTableByStore : IItemQuery<List<TableBaseDto>>
{
    public string StoreCode { get; set; }
    public sealed class Handler : IQueryHandler<GetTableByStore, ResultModel<List<TableBaseDto>>>
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly ITypeBidaRepository _typeBidaRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IStoreRepository storeRepository,
            ITableRepository tableRepository,
            IAreaRepository areaRepository,
            ITypeSaleRepository typeSaleRepository,
            ITypeBidaRepository typeBidaRepository,
            IAppContextAccessor appContextAccessor)
        {
            _storeRepository = storeRepository;
            _areaRepository = areaRepository;
            _tableRepository = tableRepository;
            _typeSaleRepository = typeSaleRepository;
            _typeBidaRepository = typeBidaRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<List<TableBaseDto>>> Handle(GetTableByStore query, CancellationToken cancellationToken)
        {
            List<TableBaseDto> result = new();
            TableByStoreCodeSpec tableSpec = new(query.StoreCode);
            List<Entities.Tables> table = await _tableRepository.FindAsync(tableSpec);
            result = table.Adapt<List<TableBaseDto>>();

            return ResultModel<List<TableBaseDto>>.Create(result);
        }
    }
}
