using Microsoft.Extensions.DependencyInjection;
using ZajeciaREST.Application.Interfaces;
using ZajeciaREST.Application.Services;

namespace ZajeciaREST.Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProdcutService, ProductService>()
            .AddScoped<ICartService, CartService>();

        return services;
    }
}
