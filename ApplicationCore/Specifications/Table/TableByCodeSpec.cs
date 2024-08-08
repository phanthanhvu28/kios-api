using ApplicationCore.Contracts.SpecificationBase;

namespace ApplicationCore.Specifications.Table;
public class TableByCodeSpec : SpecificationBase<Entities.Tables>
{
    public TableByCodeSpec(string code)
    {
        ApplyFilter(entity => entity.Code == code);
        ApplyFilter(entity => entity.IsDelete == false);
    }
}
