using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
