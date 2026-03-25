using Finances.Infrastructure.Configuration;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finances.Infrastructure.Extensions;

public static class MessagingExtension {
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration) {
        services.AddMassTransit(x => {
            var rabbitSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>() 
                                 ?? new RabbitMqSettings();
            
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) => {
                cfg.Host(rabbitSettings.Host, rabbitSettings.VirtualHost, h =>
                {
                    h.Username(rabbitSettings.Username);
                    h.Password(rabbitSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}