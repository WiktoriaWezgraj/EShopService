using EShop.Application;
using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace EShopService;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<ICreditCardService, CreditCardService>();
        builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();

        //nie wiem czy potrzebne
        builder.Services.AddDbContext<DataContext>();

        var app = builder.Build();

        using (var context = new DataContext())
        {
            context.Database.EnsureCreated();
        }

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "redis:6379"; // u¿ywamy nazwy kontenera, NIE localhost
        });


        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}