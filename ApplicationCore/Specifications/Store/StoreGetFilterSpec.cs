using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Store;
public class StoreGetFilterSpec : SpecificationBase<Entities.Stores>
{
    public StoreGetFilterSpec()
    {
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
