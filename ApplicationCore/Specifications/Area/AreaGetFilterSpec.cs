using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Area;
public class AreaGetFilterSpec : SpecificationBase<Entities.Areas>
{
    public AreaGetFilterSpec()
    {
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
