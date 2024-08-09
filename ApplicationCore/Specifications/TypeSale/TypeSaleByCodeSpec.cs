using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.TypeSale;
public class TypeSaleByCodeSpec : SpecificationBase<Entities.TypeSales>
{
    public TypeSaleByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
