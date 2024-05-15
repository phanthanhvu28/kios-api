using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class TypeBidaRepository : RepositoryBase<TypeBida>, ITypeBidaRepository
{
    public TypeBidaRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
