using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Contracts;
using Service;
using Service.Contracts;
using System.Text;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection services) =>
    services.AddDbContext<RepositoryContext>(opts =>
        opts.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")!, 
            b => b.MigrationsAssembly("API")));
    
    public static void InstantiateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
        context.Database.Migrate();
    }
}
