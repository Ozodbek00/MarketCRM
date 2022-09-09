using Market.Data.DbContexts;
using Market.Data.IRepositories;
using Market.Domain.Entities;

namespace Market.Data.Repositories
{
    public class PurchaseRepository : GenericRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MarketDbContext dbContext) : base(dbContext)
        {
        }
    }
}
