using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Area;
public class AreaByCodeSpec : SpecificationBase<Entities.Areas>
{
    public AreaByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
