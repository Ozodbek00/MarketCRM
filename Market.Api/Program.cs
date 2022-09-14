using Market.Api.Extensions;
using Market.Api.Middlewares;
using Market.Data.DbContexts;
using Market.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarketDb")));



builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtService(builder.Configuration);

// Services 
builder.Services.AddCustomServices();


//Log

builder.Logging.AddLog4Net("log4net.config");
builder.Logging.SetMinimumLevel(LogLevel.Error);

//var logger = new LoggerConfiguration()
//        .ReadFrom.Configuration(builder.Configuration)
//        .Enrich.FromLogContext()
//        .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<MarketMiddleware>();

//app.Use(async (context, next) => 
//{ 
//    await context.Response.WriteAsync("hello");

//    await next(context);
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
