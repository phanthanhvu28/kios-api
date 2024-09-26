using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Payment;
public class PaymentByCodeSpec : SpecificationBase<Entities.Payments>
{
    public PaymentByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
