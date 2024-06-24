using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class AuthenUserRepository : RepositoryBase<AuthenUser>, IAuthenUserRepository
{
    public AuthenUserRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
