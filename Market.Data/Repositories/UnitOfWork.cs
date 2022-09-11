using Market.Data.DbContexts;
using Market.Data.IRepositories;
using Market.Domain.Entities;

namespace Market.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MarketDbContext _dbContext;

        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<Category> Categories { get; }
        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Order> Orders { get; }
        public IGenericRepository<Purchase> Purchases { get; }

        public UnitOfWork(MarketDbContext dbContext)
        {
            _dbContext = dbContext;

            Products = new GenericRepository<Product>(_dbContext);
            Categories = new GenericRepository<Category>(_dbContext);
            Users = new GenericRepository<User>(_dbContext);
            Orders = new GenericRepository<Order>(_dbContext);
            Purchases = new GenericRepository<Purchase>(_dbContext);
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
