using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.TypeBida;
public class TypeBidaByArrayCodeSpec : SpecificationBase<Entities.TypeBida>
{
    public TypeBidaByArrayCodeSpec(string[] codes)
    {
        ApplyFilter(entity => codes.Any(e => e == entity.Code));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
