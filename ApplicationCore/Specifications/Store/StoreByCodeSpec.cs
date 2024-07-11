using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Store;
public class StoreByCodeSpec : SpecificationBase<Entities.Stores>
{
    public StoreByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
