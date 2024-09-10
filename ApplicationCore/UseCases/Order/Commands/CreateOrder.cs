using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Order;
using ApplicationCore.UseCases.Order.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using OneOf;
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

            OrderByCodeSpec orderSpec = new(command.OrderCode);
            Entities.Orders? orderCheck = await _orderRepository.FindOneAsync(orderSpec);
            if (orderCheck is null)
            {
                OneOf<bool, CommonExceptionBase, Entities.Orders> orderResult = await NewOrder(command);
                if (orderResult.IsT1)
                {
                    return ResultModel<bool>.Create(orderResult.AsT1);
                }

                order = orderResult.AsT2;

                OneOf<bool, CommonExceptionBase> orderDetailResult = await NewOrderDetail(order.Code, command.OrderDetail);
                if (orderDetailResult.IsT1)
                {
                    return ResultModel<bool>.Create(orderDetailResult.AsT1);
                }

            }
            else
            {
                OneOf<bool, CommonExceptionBase> orderDetailResult = await NewOrderDetail(orderCheck.Code, command.OrderDetail);
                if (orderDetailResult.IsT1)
                {
                    return ResultModel<bool>.Create(orderDetailResult.AsT1);
                }
                order = orderCheck;
            }

            OrderDetailByOrderSpec orderDetailSpec = new(order.Code);
            List<Entities.OrderDetails> orderDetailNew = await _orderDetailRepository.FindAsync(orderDetailSpec);
            decimal totalCost = orderDetailNew.Sum(d => d.Amount);

            order.TotalCost = totalCost;
            _ = await _orderRepository.UpdateAsync(order);

            return ResultModel<bool>.Create(true);
        }
        private async ValueTask<OneOf<bool, CommonExceptionBase, Entities.Orders>> NewOrder(
            CreateOrder command)
        {
            Entities.Orders order = command.Adapt<Entities.Orders>();
            OneOf<bool, CommonExceptionBase> process = order.ProcessStep(new OrderProcess(_identityUser));
            if (process.IsT1)
            {
                return process.AsT1;
            }
            order.OrderDate = DateTime.UtcNow.Date;
            order.StartTime = DateTime.UtcNow;
            order.StaffCode = "";

            bool orderResult = await _orderRepository.UpdateAsync(order);
            if (orderResult)
            {
                return order;
            }
            return true;
        }
        private async ValueTask<OneOf<bool, CommonExceptionBase>> NewOrderDetail(
           string order,
           CreateOrderDetailModel? orderDetailNew)
        {
            if (orderDetailNew != null)
            {
                Entities.OrderDetails orderDetail = orderDetailNew.Adapt<Entities.OrderDetails>();
                OneOf.OneOf<bool, CommonExceptionBase> processDetail = orderDetail.ProcessStep(new OrderProcess(_identityUser));
                if (processDetail.IsT1)
                {
                    return processDetail.AsT1;
                }
                orderDetail.OrderCode = order;
                orderDetail.StaffCode = "";
                orderDetail.Amount = orderDetail.Quantity * orderDetail.UnitPrice;
                _ = await _orderDetailRepository.UpdateAsync(orderDetail);
            }

            return true;
        }
    }
}
