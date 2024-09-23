using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Payment;
public class PaymentByOrderSpec : SpecificationBase<Entities.Payments>
{
    public PaymentByOrderSpec(string orderCode)
    {
        ApplyFilter(entity => entity.OrderCode == orderCode);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
