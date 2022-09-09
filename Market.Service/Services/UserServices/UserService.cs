using AutoMapper;
using Market.Data.IRepositories;
using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.UserDtos;
using Market.Service.Helpers;
using Market.Service.Interfaces.IUserServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Service.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> AddAsync(UserForCreation dto)
        {
            var mappedUser = _mapper.Map<User>(dto);

            mappedUser.CreatedAt = DateTime.UtcNow;

            var user = await _unitOfWork.Users.CreateAsync(mappedUser);

            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            if (await _unitOfWork.Users.GetAsync(expression) is null)
                return false;

            await _unitOfWork.Users.DeleteAsync(expression);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
            => await _unitOfWork.Users.GetAllAsync(expression).ToPagedList(@params).ToListAsync();

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression = null)
            => await _unitOfWork.Users.GetAsync(expression);

        public async Task<User> UpdateAsync(long id, UserForCreation dto)
        {
            var oldUser = await _unitOfWork.Users.GetAsync(p => p.Id == id);

            if (oldUser is null)
                return null;

            oldUser = _mapper.Map(dto, oldUser);

            oldUser.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Users.UpdateAsync(oldUser);

            await _unitOfWork.SaveChangesAsync();

            return oldUser;
        }
    }
}
