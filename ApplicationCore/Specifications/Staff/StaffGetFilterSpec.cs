using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Staff;
public class StaffGetFilterSpec : SpecificationBase<Entities.Staffs>
{
    public StaffGetFilterSpec()
    {
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
