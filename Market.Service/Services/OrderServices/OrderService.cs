using AutoMapper;
using Market.Data.IRepositories;
using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.OrderDtos;
using Market.Service.Helpers;
using Market.Service.Interfaces.IOrderService;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Service.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Order> AddAsync(OrderForCreation dto)
        {
            var mappedOrder = _mapper.Map<Order>(dto);

            mappedOrder.CreatedAt = DateTime.UtcNow;

            var order = await _unitOfWork.Orders.CreateAsync(mappedOrder);

            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            if (await _unitOfWork.Orders.GetAsync(expression) is null)
                return false;

            await _unitOfWork.Orders.DeleteAsync(expression);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(PaginationParams @params, Expression<Func<Order, bool>> expression = null)
            => await _unitOfWork.Orders.GetAllAsync(expression, "User", isTracking: false).ToPagedList(@params).ToListAsync();

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> expression = null)
            => await _unitOfWork.Orders.GetAsync(expression, "User");

        public async Task<Order> UpdateAsync(long id, OrderForCreation dto)
        {
            var oldOrder = await _unitOfWork.Orders.GetAsync(order => order.Id == id);

            if (oldOrder is null)
                return null;

            oldOrder = _mapper.Map(dto, oldOrder);

            oldOrder.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Orders.UpdateAsync(oldOrder);

            await _unitOfWork.SaveChangesAsync();

            return oldOrder;
        }
    }
}
