using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.PurchaseDtos;
using System.Linq.Expressions;

namespace Market.Service.Interfaces.IPurchaseServices
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetAllAsync(PaginationParams @params, Expression<Func<Purchase, bool>> expression = null);
        Task<Purchase> GetAsync(Expression<Func<Purchase, bool>> expression = null);
        Task<Purchase> AddAsync(PurchaseForCreation dto);
        Task<Purchase> UpdateAsync(long id, PurchaseForCreation dto);
        Task<bool> DeleteAsync(Expression<Func<Purchase, bool>> expression);
    }
}
