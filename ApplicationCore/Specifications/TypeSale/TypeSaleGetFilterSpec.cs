using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.TypeSale;
public class TypeSaleGetFilterSpec : SpecificationBase<Entities.TypeSales>
{
    public TypeSaleGetFilterSpec()
    {
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
