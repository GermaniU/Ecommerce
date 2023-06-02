using Ecommerce.Application.Persistence;
using Ecommerce.Infrastructure.Persistence;
using Ecommerce.Model.Token.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Repositories;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        return services;
    }
}