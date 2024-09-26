using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Order;
public class OrderDetailByCodeSpec : SpecificationBase<Entities.OrderDetails>
{
    public OrderDetailByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
