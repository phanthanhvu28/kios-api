using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class StaffRepository : RepositoryBase<Staffs>, IStaffRepository
{
    public StaffRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
