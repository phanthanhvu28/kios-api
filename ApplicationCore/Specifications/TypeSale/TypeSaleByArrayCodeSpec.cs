using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.TypeSale;
public class TypeSaleByArrayCodeSpec : SpecificationBase<Entities.TypeSales>
{
    public TypeSaleByArrayCodeSpec(string[] codes)
    {
        ApplyFilter(entity => codes.Any(e => e == entity.Code));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
