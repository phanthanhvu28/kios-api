using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.Specifications.Order;
using ApplicationCore.Specifications.Payment;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using OneOf;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Payment.Commands;
public class DeletePayment : VELA.WebCoreBase.Core.Mediators.ICommand<string>
{
    public string Code { get; set; }
    public string OrderCode { get; set; }
    public sealed class Handler : ICommandHandler<DeletePayment, ResultModel<string>>
    {
        private readonly IdentityUserObject? _identityUser;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        public Handler(IAppContextAccessor appContextAccessor,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository)
        {
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
        }
        public async ValueTask<ResultModel<string>> Handle(DeletePayment command, CancellationToken cancellationToken)
        {
            OneOf<bool, CommonExceptionBase, Entities.Orders> orderResult = await ValidOrder(command.OrderCode);
            if (orderResult.IsT1)
            {
                return ResultModel<string>.Create(orderResult.AsT1);
            }

            PaymentByCodeSpec paymentSpec = new(command.Code);
            Entities.Payments? payment = await _paymentRepository.FindOneAsync(paymentSpec);
            if (payment == null)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Notfound payment:[{command.Code}]"));
            }

            OneOf.OneOf<bool, CommonExceptionBase> process = payment.ProcessStep(new DeleteProcess(_identityUser));
            if (process.IsT1)
            {
                return ResultModel<string>.Create(process.AsT1);
            }

            bool result = await _paymentRepository.UpdateAsync(payment);
            if (!result)
            {
                return ResultModel<string>.Create(new NotFoundException(100036, $"Delete payment:[{payment.Amount.ToString("#,##0")}] error"));
            }

            _ = await UpdateStatusOrder(orderResult.AsT2);

            return ResultModel<string>.Create($"Delete payment [{payment.Amount.ToString("#,##0")}] success");
        }
        private async ValueTask<OneOf<bool, CommonExceptionBase>> UpdateStatusOrder(Entities.Orders order)
        {
            PaymentByOrderSpec paymentByOrderSpec = new(order.Code);
            List<Entities.Payments> paymentsByOrder = await _paymentRepository.FindAsync(paymentByOrderSpec);
            if (paymentsByOrder is { Count: 0 })
            {
                order.Status = Constants.Order.Status.New;
            }
            else
            {
                order.Status = Constants.Order.Status.Partial;
            }
            _ = await _orderRepository.UpdateAsync(order);

            return true;
        }

        private async ValueTask<OneOf<bool, CommonExceptionBase, Entities.Orders>> ValidOrder(string orderCode)
        {
            OrderByCodeSpec orderSpec = new(orderCode);
            Entities.Orders? order = await _orderRepository.FindOneAsync(orderSpec);
            if (order is null)
            {
                return new NotFoundException(100036, $"Order not found {orderCode}");
            }
            if (order.Status == Constants.Order.Status.Finish)
            {
                return new NotFoundException(100036, $"Order {orderCode} payment is finished");
            }

            return order;
        }
    }
}
