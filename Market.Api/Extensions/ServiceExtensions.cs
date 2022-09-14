using Market.Data.IRepositories;
using Market.Data.Repositories;
using Market.Service.Interfaces;
using Market.Service.Interfaces.IAuthServices;
using Market.Service.Interfaces.IOrderService;
using Market.Service.Interfaces.IPurchaseServices;
using Market.Service.Interfaces.IUserServices;
using Market.Service.Services.AuthServices;
using Market.Service.Services.OrderServices;
using Market.Service.Services.ProductServices;
using Market.Service.Services.PurchaseServices;
using Market.Service.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(p =>
            {
                var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
