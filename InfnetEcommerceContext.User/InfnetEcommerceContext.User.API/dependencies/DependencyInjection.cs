using InfnetEcommerceContext.User.API.Repository.DataContext;
using InfnetEcommerceContext.User.API.Repository.Repositories;
using InfnetEcommerceContext.User.API.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InfnetEcommerceContext.User.API.dependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            return services;
        }

        public static IServiceCollection AddUserDatabase(this IServiceCollection services, string ConnectionString)
        {

            services.AddDbContext<UserContext>(c =>
            {
                c.UseSqlServer(ConnectionString);
            });

            return services;
        }

        public static IServiceCollection InjectUserRabbitMq(this IServiceCollection services, WebApplicationBuilder builder)
        {

            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                var entryAssembly = Assembly.GetEntryAssembly();

                x.AddConsumers(entryAssembly);
                x.AddActivities(entryAssembly);
                x.UsingRabbitMq((cxt, cfg) =>
                {
                    cfg.Host("rabbitmq", h =>
                    {
                        h.Username(builder.Configuration.GetValue<string>("rabbitmq.login"));
                        h.Password(builder.Configuration.GetValue<string>("rabbitmq.password"));
                    });

                    cfg.ConfigureEndpoints(cxt);
                });
            });

            return services;
        }
    }
}
