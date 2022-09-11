using AutoMapper;
using Market.Data.IRepositories;
using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.Helpers;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Service.Services.ProductServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Category> AddCategoryAsync(string dto)
        {
            Category category = new Category();

            category.Name = dto;

            category.CreatedAt = DateTime.UtcNow;

            var result = await _unitOfWork.Categories.CreateAsync(category);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteCategoryAsync(Expression<Func<Category, bool>> expression)
        {
            if (await _unitOfWork.Categories.GetAsync(expression) is null)
                return false;

            await _unitOfWork.Categories.DeleteAsync(expression);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null)
        => await _unitOfWork.Categories.GetAllAsync(expression, isTracking: false).ToPagedList(@params).ToListAsync();

        public async Task<IEnumerable<Category>> GetAllCategoryWithProductsAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null)
            => await _unitOfWork.Categories.GetAllAsync(expression, "Product", isTracking: false).ToPagedList(@params).ToListAsync();

        public async Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> expression = null)
            => await _unitOfWork.Categories.GetAsync(expression);

        public async Task<Category> UpdateCategoryAsync(long id, string dto)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(p => p.Id == id);

            if (oldCategory is null)
                return null;

            oldCategory = _mapper.Map(dto, oldCategory);

            oldCategory.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Categories.UpdateAsync(oldCategory);

            await _unitOfWork.SaveChangesAsync();

            return oldCategory;
        }
    }
}
