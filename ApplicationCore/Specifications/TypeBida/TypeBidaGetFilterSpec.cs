using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.TypeBida;
public class TypeBidaGetFilterSpec : SpecificationBase<Entities.TypeBida>
{
    public TypeBidaGetFilterSpec()
    {
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
