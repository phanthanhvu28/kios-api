using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Order;
public class OrderByCodeSpec : SpecificationBase<Entities.Orders>
{
    public OrderByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
