using Market.Data.DbContexts;
using Market.Data.IRepositories;
using Market.Domain.Entities;

namespace Market.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MarketDbContext dbContext) : base(dbContext)
        {
        }
    }
}
