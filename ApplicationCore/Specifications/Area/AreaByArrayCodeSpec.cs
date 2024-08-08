using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Area;
public class AreaByArrayCodeSpec : SpecificationBase<Entities.Areas>
{
    public AreaByArrayCodeSpec(string[] codes)
    {
        ApplyFilter(entity => codes.Any(e => e == entity.Code));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
