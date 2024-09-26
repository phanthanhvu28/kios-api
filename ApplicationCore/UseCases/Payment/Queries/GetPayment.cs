using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.DTOs.Payment;
using ApplicationCore.Specifications.Payment;
using ApplicationCore.ValueObjects;
using Mapster;
using Mediator;
using VELA.WebCoreBase.Core.Models;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore.UseCases.Payment.Queries;
public class GetPayment : VELA.WebCoreBase.Core.Mediators.IItemQuery<List<GetPaymentDto>>
{
    public string OrderCode { get; set; }
    public sealed class Handler : IQueryHandler<GetPayment, ResultModel<List<GetPaymentDto>>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IdentityUserObject? _identityUser;

        public Handler(
            IPaymentRepository paymentRepository,
            IAppContextAccessor appContextAccessor)
        {
            _paymentRepository = paymentRepository;
            _identityUser = appContextAccessor.IdentityUser?.Adapt<IdentityUserObject>();
        }
        public async ValueTask<ResultModel<List<GetPaymentDto>>> Handle(GetPayment query, CancellationToken cancellationToken)
        {

            PaymentByOrderSpec paymentSpec = new(query.OrderCode);
            List<Entities.Payments> payments = await _paymentRepository.FindAsync(paymentSpec);

            List<GetPaymentDto> result = payments.Adapt<List<GetPaymentDto>>();

            return ResultModel<List<GetPaymentDto>>.Create(result);
        }
    }
}
