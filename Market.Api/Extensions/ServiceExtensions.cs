using Market.Data.IRepositories;
using Market.Data.Repositories;
using Market.Service.Interfaces;
using Market.Service.Interfaces.IOrderService;
using Market.Service.Interfaces.IPurchaseServices;
using Market.Service.Interfaces.IUserServices;
using Market.Service.Services.OrderServices;
using Market.Service.Services.ProductServices;
using Market.Service.Services.PurchaseServices;
using Market.Service.Services.UserServices;

namespace Market.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
        }
    }
}
