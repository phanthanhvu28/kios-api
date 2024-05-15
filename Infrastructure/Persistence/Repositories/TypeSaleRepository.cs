using ApplicationCore.Contracts.RepositoryBase;
using ApplicationCore.Entities;

namespace Infrastructure.Persistence.Repositories;
public class TypeSaleRepository : RepositoryBase<TypeSales>, ITypeSaleRepository
{
    public TypeSaleRepository(MainDbContext dbContext) : base(dbContext)
    {
    }
}
