using Market.Domain.Configurations;
using Market.Domain.Entities;
using System.Linq.Expressions;

namespace Market.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null);
        Task<IEnumerable<Category>> GetAllCategoryWithProductsAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null);
        Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> expression = null);
        Task<Category> AddCategoryAsync(string category);
        Task<Category> UpdateCategoryAsync(long id, string dto);
        Task<bool> DeleteCategoryAsync(Expression<Func<Category, bool>> expression);
    }
}
