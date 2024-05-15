using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class AreaRepository : RepositoryBase<Areas>, IAreaRepository
{
    public AreaRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
