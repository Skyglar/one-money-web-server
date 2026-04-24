using EasyNetQ;
using Finances.Infrastructure.Configuration;
using Finances.Infrastructure.Messaging.Consumers;
using Finances.Infrastructure.Messaging.HostedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finances.Infrastructure.Extensions;

public static class MessagingExtension {
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration) {
        var rabbitSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>()
                             ?? new RabbitMqSettings();

        services.AddEasyNetQ(BuildConnectionString(rabbitSettings));
        services.AddHostedService<TransactionCreatedSubscriberHostedService>();
        services.AddScoped<TransactionCreatedConsumer>();

        return services;
    }

    private static string BuildConnectionString(RabbitMqSettings settings) {
        var virtualHost = string.IsNullOrWhiteSpace(settings.VirtualHost) ? "/" : settings.VirtualHost;

        return $"host={settings.Host};virtualHost={virtualHost};username={settings.Username};password={settings.Password}";
    }
}
