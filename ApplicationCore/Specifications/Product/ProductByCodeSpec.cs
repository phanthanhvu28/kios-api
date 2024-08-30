using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Product;
public class ProductByCodeSpec : SpecificationBase<Entities.Products>
{
    public ProductByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
