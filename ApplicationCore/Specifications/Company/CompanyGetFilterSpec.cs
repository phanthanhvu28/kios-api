using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Company;
public class CompanyGetFilterSpec : SpecificationBase<Entities.Companies>
{
    public CompanyGetFilterSpec()
    {
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
