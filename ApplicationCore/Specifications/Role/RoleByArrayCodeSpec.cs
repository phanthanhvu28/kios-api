using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Role;
public class RoleByArrayCodeSpec : SpecificationBase<Entities.Role>
{
    public RoleByArrayCodeSpec(string[] codes)
    {
        ApplyFilter(entity => codes.Any(e => e == entity.Code));
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
