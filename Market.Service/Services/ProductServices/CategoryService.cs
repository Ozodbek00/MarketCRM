using AutoMapper;
using Market.Data.IRepositories;
using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.Interfaces;
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

        public Task<bool> DeleteCategoryAsync(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllCategoriesAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllCategoryWithProductsAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryAsync(Expression<Func<Category, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategoryAsync(long id, string dto)
        {
            throw new NotImplementedException();
        }
    }
}
