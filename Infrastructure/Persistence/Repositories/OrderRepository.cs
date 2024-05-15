using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class OrderRepository : RepositoryBase<Orders>, IOrderRepository
{
    public OrderRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
