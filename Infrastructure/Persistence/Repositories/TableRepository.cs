using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class TableRepository : RepositoryBase<Tables>, ITableRepository
{
    public TableRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
