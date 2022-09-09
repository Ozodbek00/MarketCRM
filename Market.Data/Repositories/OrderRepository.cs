using Market.Data.DbContexts;
using Market.Data.IRepositories;
using Market.Domain.Entities;

namespace Market.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(MarketDbContext dbContext) : base(dbContext)
        {
        }
    }
}
