using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Staff;
public class StaffByCodeSpec : SpecificationBase<Entities.Staffs>
{
    public StaffByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
