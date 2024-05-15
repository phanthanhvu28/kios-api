using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class CompanyRepository : RepositoryBase<Companies>, ICompanyRepository
{
    public CompanyRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
