using Market.Domain.Entities;

namespace Market.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<User> Users { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Purchase> Purchases { get; }

        Task SaveChangesAsync();
    }
}
