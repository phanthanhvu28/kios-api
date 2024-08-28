using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Table;
public class TableByStoreCodeSpec : SpecificationBase<Entities.Tables>
{
    public TableByStoreCodeSpec(string storeCode)
    {
        ApplyFilter(entity => entity.StoreCode == storeCode);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
