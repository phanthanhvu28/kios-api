using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.TypeBida;
public class TypeBidaByCodeSpec : SpecificationBase<Entities.TypeBida>
{
    public TypeBidaByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
