using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class OrderDetailRepository : RepositoryBase<OrderDetails>, IOrderDetailRepository
{
    public OrderDetailRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
