using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Store;
public class StoreByArrayCodeSpec : SpecificationBase<Entities.Stores>
{
    public StoreByArrayCodeSpec(string[] codes)
    {
        ApplyFilter(entity => codes.Any(e => e == entity.Code));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
