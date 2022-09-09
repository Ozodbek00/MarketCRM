using AutoMapper;
using Market.Data.IRepositories;
using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.ProductDtos;
using Market.Service.Helpers;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Service.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Product> AddAsync(ProductForCreation dto)
        {
            var mappedProduct = _mapper.Map<Product>(dto);

            mappedProduct.CreatedAt = DateTime.UtcNow;

            var product = await _unitOfWork.Products.CreateAsync(mappedProduct);

            await _unitOfWork.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
        {
            if (await _unitOfWork.Products.GetAsync(expression) is null)
                return false;

            await _unitOfWork.Products.DeleteAsync(expression);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
            => await _unitOfWork.Products.GetAllAsync(expression, isTracking: false).ToPagedList(@params).ToListAsync();

        public async Task<IEnumerable<Product>> GetAllWithCategoriesAsync(PaginationParams @params, Expression<Func<Product, bool>> expression = null)
            => await _unitOfWork.Products.GetAllAsync(expression, "Category", isTracking: false)
            .ToPagedList(@params).ToListAsync();

        public async Task<Product> GetAsync(Expression<Func<Product, bool>> expression = null)
            => await _unitOfWork.Products.GetAsync(expression, "Category");

        public async Task<Product> UpdateAsync(long id, ProductForCreation dto)
        {
            var oldProduct = await _unitOfWork.Products.GetAsync(p => p.Id == id);

            if (oldProduct is null)
                return null;

            oldProduct = _mapper.Map(dto, oldProduct);

            oldProduct.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Products.UpdateAsync(oldProduct);

            await _unitOfWork.SaveChangesAsync();

            return oldProduct;
        }
    }
}
