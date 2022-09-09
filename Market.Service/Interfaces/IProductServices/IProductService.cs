using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.ProductDtos;
using System.Linq.Expressions;

namespace Market.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null);
        Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null);
        Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null);
        Task<Product> AddAsync(ProductForCreation dto);
        Task<Product> UpdateAsync(long id, ProductForCreation dto);
        Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression);
    }
}
