using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class StoreRepository : RepositoryBase<Stores>, IStoreRepository
{
    public StoreRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
