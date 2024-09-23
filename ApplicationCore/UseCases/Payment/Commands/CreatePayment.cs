using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DomainBusiness;
using ApplicationCore.DTOs.Payment;
using ApplicationCore.Specifications.Order;
using ApplicationCore.Specifications.Payment;
using ApplicationCore.UseCases.Payment.Models;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using OneOf;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace ApplicationCore.UseCases.Payment.Commands;
public class CreatePayment : CreatePaymentModel, VELA.WebCoreBase.Core.Mediators.ICommand<CreatePaymentDto>
{
    public sealed class Handler : ICommandHandler<CreatePayment, ResultModel<CreatePaymentDto>>
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
        public async ValueTask<ResultModel<CreatePaymentDto>> Handle(CreatePayment command, CancellationToken cancellationToken)
        {
            OrderByCodeSpec orderSpec = new(command.OrderCode);
            Entities.Orders? order = await _orderRepository.FindOneAsync(orderSpec);
            if (order is null)
            {
                return ResultModel<CreatePaymentDto>.Create(new NotFoundException(100036, $"Order not found {command.OrderCode}"));
            }
            if (order.Status == Constants.Order.Status.Finish)
            {
                return ResultModel<CreatePaymentDto>.Create(new NotFoundException(100036, $"Order {command.OrderCode} payment is finished"));
            }

            OneOf<bool, CommonExceptionBase, Entities.Payments> paymentResult = await Payment(command);
            if (paymentResult.IsT1)
            {
                return ResultModel<CreatePaymentDto>.Create(paymentResult.AsT1);
            }

            _ = await UpdateStatusOrder(order);

            CreatePaymentDto resultDto = paymentResult.AsT2.Adapt<CreatePaymentDto>();
            return ResultModel<CreatePaymentDto>.Create(resultDto);
        }
        private async ValueTask<OneOf<bool, CommonExceptionBase, Entities.Payments>> Payment(
            CreatePayment command)
        {
            Entities.Payments payment = command.Adapt<Entities.Payments>();
            OneOf<bool, CommonExceptionBase> process = payment.ProcessStep(new PaymentProcess(_identityUser));
            if (process.IsT1)
            {
                return process.AsT1;
            }

            payment.PaymentDate = DateTime.UtcNow.Date;

            bool orderResult = await _paymentRepository.UpdateAsync(payment);
            if (orderResult)
            {
                return payment;
            }
            return true;
        }
        private async ValueTask<OneOf<bool, CommonExceptionBase>> UpdateStatusOrder(
           Entities.Orders order)
        {
            PaymentByOrderSpec paymentSpec = new(order.Code);
            List<Entities.Payments> payments = await _paymentRepository.FindAsync(paymentSpec);

            if (order.TotalCost <= payments.Sum(d => d.Amount))
            {
                order.Status = Constants.Order.Status.Finish;
            }
            else
            {
                order.Status = Constants.Order.Status.Partial;
            }
            _ = await _orderRepository.UpdateAsync(order);
            return true;
        }
    }
}
