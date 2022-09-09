using Market.Data.DbContexts;
using Market.Data.IRepositories;
using Market.Domain.Entities;

namespace Market.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MarketDbContext dbContext) : base(dbContext)
        {
        }
    }
}
