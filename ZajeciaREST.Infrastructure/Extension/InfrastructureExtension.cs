using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZajeciaREST.Application.Infrastructure;
using ZajeciaREST.Infrastructure.Entity;
using ZajeciaREST.Infrastructure.Repositories;
using ZajeciaREST.Infrastructure.Settings;

namespace ZajeciaREST.Infrastructure.Extension;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, InfrastructureSettings settings)
    {

        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            
        services.AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<ICartRepository, CartRepository>();

        return services;
    }
}
