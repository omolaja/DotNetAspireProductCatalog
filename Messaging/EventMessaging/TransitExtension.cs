using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventMessaging
{
   public static class TransitExtension
    {
        public static IServiceCollection AddTransitWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                config.AddConsumers(assemblies);
                config.AddSagas(assemblies);
                config.AddActivities(assemblies);
                config.AddSagaStateMachines(assemblies);

                config.UsingRabbitMq((context, cfg) =>
                {
                    //cfg.Host("localhost", "/", h =>
                    //{
                    //    h.Username("guest");
                    //    h.Password("guest");
                    //});
                    //cfg.ConfigureEndpoints(context);
                    var configuraion = context.GetRequiredService<IConfiguration>();
                    var connectionString = configuraion.GetConnectionString("rabbitmq");

                    cfg.Host(connectionString);
                    cfg.ConfigureEndpoints(context);
                });

            });
            return services;
        }
    }
}
