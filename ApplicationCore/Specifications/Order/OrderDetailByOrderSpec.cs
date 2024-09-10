using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Order;
public class OrderDetailByOrderSpec : SpecificationBase<Entities.OrderDetails>
{
    public OrderDetailByOrderSpec(string orderCode)
    {
        ApplyFilter(entity => entity.OrderCode == orderCode);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
