using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Order;
using ApplicationCore.UseCases.Order.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Order.Commands;
public sealed class CreateOrder : CreateOrderModel, VELA.WebCoreBase.Core.Mediators.ICommand<bool>
{
    public sealed class Handler : ICommandHandler<CreateOrder, ResultModel<bool>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly string[] Status = { Constants.Order.Status.New, Constants.Order.Status.Partial };
        public Handler(IAppContextAccessor appContextAccessor,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public async ValueTask<ResultModel<bool>> Handle(CreateOrder command, CancellationToken cancellationToken)
        {
            Entities.Orders order = command.Adapt<Entities.Orders>();
            OneOf.OneOf<bool, CommonExceptionBase> process = order.ProcessStep(new CreateNewProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<bool>.Create(process.AsT1);
            }
            order.OrderDate = DateTime.UtcNow.Date;
            order.StartTime = DateTime.UtcNow;
            order.StaffCode = "";
            OrderByCodeStatusSpec orderSpec = new(order.Code, order.StaffCode);

            _ = await _orderRepository.UpdateAsync(order);
            if (command.OrderDetail != null)
            {
                Entities.OrderDetails orderDetail = command.OrderDetail.Adapt<Entities.OrderDetails>();
                OneOf.OneOf<bool, CommonExceptionBase> processDetail = orderDetail.ProcessStep(new CreateNewProcess(_identityUser));
                if (processDetail.IsT1)
                {
                    return ResultModel<bool>.Create(processDetail.AsT1);
                }
                orderDetail.OrderCode = order.Code;
                orderDetail.StaffCode = "";
                orderDetail.Amount = orderDetail.Quantity * orderDetail.UnitPrice;
                _ = await _orderDetailRepository.UpdateAsync(orderDetail);
            }

            return ResultModel<bool>.Create(true);
        }
    }
}
