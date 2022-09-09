using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.OrderDtos;
using System.Linq.Expressions;

namespace Market.Service.Interfaces.IOrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync(PaginationParams @params, Expression<Func<Order, bool>> expression = null);
        Task<Order> GetAsync(Expression<Func<Order, bool>> expression = null);
        Task<Order> AddAsync(OrderForCreation dto);
        Task<Order> UpdateAsync(long id, OrderForCreation dto);
        Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression);
    }
}
