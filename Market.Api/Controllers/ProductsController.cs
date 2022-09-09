using Market.Domain.Configurations;
using Market.Domain.Entities;
using Market.Service.DTOs.ProductDtos;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        /// <summary>
        ///  Product CRUD
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        #region
        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _productService.GetAllAsync(@params));

        [HttpGet("Full")]
        public async ValueTask<IActionResult> GetAllWithCategoriesAsync([FromQuery] PaginationParams @params)
            => Ok(await _productService.GetAllWithCategoriesAsync(@params));

        [HttpGet("{Id}")]
        public async ValueTask<IActionResult> GetAsync([FromRoute(Name = "Id")] long id)
            => Ok(await _productService.GetAsync(p => p.Id == id));

        [HttpPost]
        public async ValueTask<ActionResult<Product>> AddAsync(ProductForCreation dto)
            => Ok(await _productService.AddAsync(dto));

        [HttpPut("{Id}")]
        public async ValueTask<ActionResult<Product>> UpdateAsync([FromRoute(Name = "Id")] long id, ProductForCreation dto)
            => Ok(await _productService.UpdateAsync(id, dto));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute(Name = "Id")] long id)
            => await _productService.DeleteAsync(p => p.Id == id);
        #endregion

        [HttpPost("Category")]
        public async ValueTask<ActionResult<Category>> AddCategoryAsync(string dto)
            => Ok(await _categoryService.AddCategoryAsync(dto));



    }
}
