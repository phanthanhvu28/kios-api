using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class PaymentRepository : RepositoryBase<Payments>, IPaymentRepository
{
    public PaymentRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
