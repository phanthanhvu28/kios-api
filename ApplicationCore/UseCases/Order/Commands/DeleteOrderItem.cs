using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Order;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using OneOf;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Order.Commands;
public class DeleteOrderItem : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public sealed class Handler : ICommandHandler<DeleteOrderItem, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderRepository _orderRepository;
        public Handler(IAppContextAccessor appContextAccessor,
            IOrderDetailRepository orderDetailRepository,
            IOrderRepository orderRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _orderDetailRepository = orderDetailRepository;
            _orderRepository = orderRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeleteOrderItem command, CancellationToken cancellationToken)
        {
            OrderDetailByCodeSpec productSpec = new(command.Code);
            Entities.OrderDetails? orderDetail = await _orderDetailRepository.FindOneAsync(productSpec);
            if (orderDetail == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound product:[{command.Code}]"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = orderDetail.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _orderDetailRepository.UpdateAsync(orderDetail);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete product:[{orderDetail.Code}] error"));
            }

            _ = await UpdateTotalOrder(orderDetail.OrderCode);

            return ResultModel<string>.Create($"Delete product [{orderDetail.ProductCode}-{orderDetail.Amount.ToString("#,##0")}] success");
        }

        private async ValueTask<OneOf<bool, CommonExceptionBase>> UpdateTotalOrder(string orderCode)
        {

            OrderByCodeSpec orderSpec = new(orderCode);
            Entities.Orders? order = await _orderRepository.FindOneAsync(orderSpec);
            if (order != null)
            {
                OrderDetailByOrderSpec orderDetailSpec = new(orderCode);
                List<Entities.OrderDetails> orderDetailNew = await _orderDetailRepository.FindAsync(orderDetailSpec);
                decimal totalCost = orderDetailNew.Sum(d => d.Amount);
                if (totalCost == 0)
                {
                    order.IsDelete = true;
                }

                order.TotalCost = totalCost;
                _ = await _orderRepository.UpdateAsync(order);
            }

            return true;
        }


    }
}
