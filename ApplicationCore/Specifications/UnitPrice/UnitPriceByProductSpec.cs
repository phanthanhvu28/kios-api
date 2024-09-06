using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.UnitPrice;
public class UnitPriceByProductSpec : SpecificationBase<Entities.SetupPrice>
{
    public UnitPriceByProductSpec(string storeCode, string productCode)
    {
        ApplyFilter(entity => entity.StoreCode == storeCode && entity.ProductCode == productCode);
        ApplyFilter(entity => entity.ValidFrom.Date <= DateTime.UtcNow.Date && entity.ValidTo.Date >= DateTime.UtcNow.Date);
        ApplySort("IdDesc");
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
