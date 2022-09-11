using AutoMapper;
using Market.Data.IRepositories;
using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.PurchaseDtos;
using Market.Service.Helpers;
using Market.Service.Interfaces.IPurchaseServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Service.Services.PurchaseServices
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PurchaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Purchase> AddAsync(PurchaseForCreation dto)
        {
            var mappedPurchase = mapper.Map<Purchase>(dto);

            mappedPurchase.CreatedAt = DateTime.UtcNow;

            var purchase = await unitOfWork.Purchases.CreateAsync(mappedPurchase);

            await unitOfWork.SaveChangesAsync();

            return purchase;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Purchase, bool>> expression)
        {
            if (await unitOfWork.Purchases.GetAsync(expression) is null)
                return false;

            await unitOfWork.Purchases.DeleteAsync(expression);

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync(PaginationParams @params, Expression<Func<Purchase, bool>> expression = null)
            => await unitOfWork.Purchases.GetAllAsync(expression, "Product", isTracking: false)
                                         .ToPagedList(@params).ToListAsync();

        public async Task<Purchase> GetAsync(Expression<Func<Purchase, bool>> expression = null)
            => await unitOfWork.Purchases.GetAsync(expression, "Product");

        public async Task<Purchase> UpdateAsync(long id, PurchaseForCreation dto)
        {
            var oldPurchase = await unitOfWork.Purchases.GetAsync(purchase => purchase.Id == id);

            if (oldPurchase is not null)
                return null;

            oldPurchase = mapper.Map(dto, oldPurchase);

            oldPurchase.UpdatedAt = DateTime.UtcNow;

            await unitOfWork.Purchases.UpdateAsync(oldPurchase);

            await unitOfWork.SaveChangesAsync();

            return oldPurchase;
        }
    }
}
