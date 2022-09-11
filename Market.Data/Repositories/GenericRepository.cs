using Market.Data.DbContexts;
using Market.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Data.Repositories
{
#pragma warning disable
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
    {
        protected readonly MarketDbContext _dbContext;
        protected readonly DbSet<TSource> _dbSet;

        public GenericRepository(MarketDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TSource>();
        }

        public async Task<TSource> CreateAsync(TSource source)
            => (await _dbSet.AddAsync(source)).Entity;

        public async Task DeleteAsync(Expression<Func<TSource, bool>> expression)
            => _dbSet.Remove(await GetAsync(expression));

        public IQueryable<TSource> GetAllAsync(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracking = true)
        {
            IQueryable<TSource> query = expression is null ? _dbSet : _dbSet.Where(expression);

            if ((!string.IsNullOrEmpty(include)))
                query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null)
            => await GetAllAsync(expression, include).FirstOrDefaultAsync();

        public async Task<TSource> UpdateAsync(TSource source)
            => _dbSet.Update(source).Entity;
    }
}
