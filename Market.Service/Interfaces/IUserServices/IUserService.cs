using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.UserDtos;
using System.Linq.Expressions;

namespace Market.Service.Interfaces.IUserServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);
        Task<User> GetAsync(Expression<Func<User, bool>> expression = null);
        Task<User> AddAsync(UserForCreation dto);
        Task<User> UpdateAsync(long id, UserForCreation dto);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    }
}
