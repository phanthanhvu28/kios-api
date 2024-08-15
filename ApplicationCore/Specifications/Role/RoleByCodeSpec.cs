using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Role;
public class RoleByCodeSpec : SpecificationBase<Entities.Role>
{
    public RoleByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
