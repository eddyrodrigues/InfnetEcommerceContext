using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using InfnetEcommerceContext.Product.API.Repository;
using InfnetEcommerceContext.Product.API.Repository.DataContext;
using InfnetEcommerceContext.Product.API.Services;
using InfnetEcommerceContext.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InfnetEcommerceContext.Product.API.Dependencies;

public static class ConfigureDependencies
{
    public static SecretClient SecretClientApp { get; private set; }
    public static SecretClient GetSecretClientForApplication()
    {
        if (SecretClientApp == null)
        {
            SecretClientApp = new SecretClient(vaultUri: new Uri(Environment.GetEnvironmentVariable("azureapiskeyvault")), credential: new DefaultAzureCredential());   
        }

        return SecretClientApp;
    }
    public static IServiceCollection AddProductsDatabase(this IServiceCollection services, string environment)
    {
        var hostDataBase = Environment.GetEnvironmentVariable("DB_CONN_STRING");

        if (Microsoft.Extensions.Hosting.Environments.Staging == environment || Microsoft.Extensions.Hosting.Environments.Production == environment)
        {
            var secret = GetSecretClientForApplication().GetSecret("dbproducts");
            hostDataBase = secret.Value?.Value;
        }

        services.AddDbContext<ProductContext>(c => c.UseSqlServer(hostDataBase));

        return services;
    }

    // TODO: May send this to shared project
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, string environment)
    {
        var tokenSecret = Environment.GetEnvironmentVariable("SECRET_TOKEN_JWT");

        if (Microsoft.Extensions.Hosting.Environments.Staging == environment || Microsoft.Extensions.Hosting.Environments.Production == environment)
        {
            var secret = GetSecretClientForApplication().GetSecret("tokensecret");
            tokenSecret = secret.Value?.Value;
        }

        services.AddJwtTokenConfiguration(tokenSecret);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
