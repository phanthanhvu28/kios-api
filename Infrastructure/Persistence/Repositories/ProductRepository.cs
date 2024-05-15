using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class ProductRepository : RepositoryBase<Products>, IProductRepository
{
    public ProductRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
