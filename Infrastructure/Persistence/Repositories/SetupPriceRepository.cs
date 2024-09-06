using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class SetupPriceRepository : RepositoryBase<SetupPrice>, ISetupPriceRepository
{
    public SetupPriceRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
