using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Order;
public class OrderByTableOutRangeStatusSpec : SpecificationBase<Entities.Orders>
{
    public OrderByTableOutRangeStatusSpec(string storeCode, string[] tableCodes, string[] status)
    {
        ApplyFilter(entity => entity.StoreCode == storeCode);
        ApplyFilter(entity => tableCodes.Any(d => d == entity.TableCode));
        ApplyFilter(entity => status.Any(d => d == entity.Status));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
