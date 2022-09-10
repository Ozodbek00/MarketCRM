using Market.Data.DbContexts;
using Market.Data.IRepositories;
using Market.Data.Repositories;
using Market.Service.Interfaces;
using Market.Service.Interfaces.IOrderService;
using Market.Service.Interfaces.IPurchaseServices;
using Market.Service.Interfaces.IUserServices;
using Market.Service.Mappers;
using Market.Service.Services.OrderServices;
using Market.Service.Services.ProductServices;
using Market.Service.Services.PurchaseServices;
using Market.Service.Services.UserServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketDb")));



builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<MarketMiddleware>();

//app.Use(async (context, next) => 
//{ 
//    await context.Response.WriteAsync("hello");

//    await next(context);
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
