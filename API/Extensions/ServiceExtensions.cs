using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contracts;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
    services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureSqlContext(this IServiceCollection services) =>
    services.AddDbContext<RepositoryContext>(opts =>
        opts.UseSqlServer(Environment.GetEnvironmentVariable("CONNECTION_STRING")!));
}
