using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Product;
public class ProductByArrayCodeSpec : SpecificationBase<Entities.Products>
{
    public ProductByArrayCodeSpec(string[] codes)
    {
        ApplyFilter(entity => codes.Any(e => e == entity.Code));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
