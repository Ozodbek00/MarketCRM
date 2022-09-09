namespace Market.Data.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        IPurchaseRepository Purchases { get; }

        Task SaveChangesAsync();
    }
}
