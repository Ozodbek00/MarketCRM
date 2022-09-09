using Market.Data.DbContexts;
using Market.Data.IRepositories;

namespace Market.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public MarketDbContext _dbContext;

        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IUserRepository Users { get; }
        public IOrderRepository Orders { get; }
        public IPurchaseRepository Purchases { get; }

        public UnitOfWork(MarketDbContext dbContext)
        {
            _dbContext = dbContext;

            Products = new ProductRepository(_dbContext);
            Categories = new CategoryRepository(_dbContext);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
