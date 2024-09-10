using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Table;
using ApplicationCore.Specifications.Order;
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
        private readonly ITableRepository _tableRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IdentityUserObject? _identityUser;

        private readonly string[] _status = { Constants.Order.Status.New, Constants.Order.Status.Partial };
        public Handler(
            ITableRepository tableRepository,
            IOrderRepository orderRepository,
            IAppContextAccessor appContextAccessor)
        {

            _tableRepository = tableRepository;
            _orderRepository = orderRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<List<TableBaseDto>>> Handle(GetTableByStore query, CancellationToken cancellationToken)
        {
            List<TableBaseDto> result = new();
            TableByStoreCodeSpec tableSpec = new(query.StoreCode);
            List<Entities.Tables> table = await _tableRepository.FindAsync(tableSpec);
            result = table.Adapt<List<TableBaseDto>>();

            string?[] tableCodes = result.Select(x => x.Code).ToArray();
            OrderByTableOutRangeStatusSpec orderTableSpec = new(query.StoreCode, tableCodes, _status);
            List<Entities.Orders> tableOrders = await _orderRepository.FindAsync(orderTableSpec);

            result.ForEach(t =>
            {
                Entities.Orders? order = tableOrders.FirstOrDefault(d => d.TableCode == t.Code);
                if (order != null)
                {
                    t.Order = new OrderPick { OrderCode = order.Code, Status = order.Status, TotalCost = order.TotalCost };
                }

            });

            return ResultModel<List<TableBaseDto>>.Create(result);
        }
    }
}
