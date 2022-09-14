using AutoMapper;
using Market.Data.IRepositories;
using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Domain.Enums;
using Market.Service.DTOs.UserDtos;
using Market.Service.Exceptions;
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
            var user = await _unitOfWork.Users.GetAsync(p =>
                p.Login.Equals(dto.Login) && p.State != ItemState.Deleted);
            if (user is not null)
                throw new CustomException(400, "User already exist");

            var mappedUser = _mapper.Map<User>(dto);

            mappedUser.CreatedAt = DateTime.UtcNow;

            mappedUser = await _unitOfWork.Users.CreateAsync(mappedUser);

            await _unitOfWork.SaveChangesAsync();

            return mappedUser;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            User user = await _unitOfWork.Users.GetAsync(expression);

            if (user is null)
                throw new CustomException(404, "User not found");

            user.State = ItemState.Deleted;
            user.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            var users = _unitOfWork.Users.GetAllAsync(expression, isTracking: false);

            return await users.ToPagedList(@params).ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression = null)
        {
            var user = await _unitOfWork.Users.GetAsync(expression);
            if (user is null)
                throw new CustomException(404, "User not found");

            return user;
        }

        public async Task<User> UpdateAsync(long id, UserForCreation dto)
        {
            var oldUser = await _unitOfWork.Users.GetAsync(p => p.Id == id
                && p.State != ItemState.Deleted);

            if (oldUser is null)
                throw new CustomException(400, "User not found");

            var existUser = await _unitOfWork.Users.GetAsync(p =>
                p.Login.Equals(dto.Login) && p.State != ItemState.Deleted);

            if (existUser is not null)
                throw new CustomException(400, "This login already exist");

            var mappedUser = _mapper.Map(dto, oldUser);

            mappedUser.State = ItemState.Updated;
            mappedUser.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _unitOfWork.Users.UpdateAsync(mappedUser);

            await _unitOfWork.SaveChangesAsync();

            return updatedUser;
        }
    }
}
