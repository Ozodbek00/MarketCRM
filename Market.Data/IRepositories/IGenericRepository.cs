using System.Linq.Expressions;

namespace Market.Data.IRepositories
{
    public interface IGenericRepository<TSource> where TSource : class
    {
        Task<TSource> CreateAsync(TSource source);
        IQueryable<TSource> GetAllAsync(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracking = true);
        Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null);
        Task<TSource> UpdateAsync(TSource source);
        Task DeleteAsync(Expression<Func<TSource, bool>> expression);
    }
}
