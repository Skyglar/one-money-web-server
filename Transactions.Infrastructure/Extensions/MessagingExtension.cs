using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transactions.Infrastructure.Configuration;
using Transactions.Infrastructure.Messaging;
using Transactions.Infrastructure.Messaging.HostedServices;

namespace Transactions.Infrastructure.Extensions;

public static class MessagingExtension {
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration) {
        var rabbitSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>()
                             ?? new RabbitMqSettings();

        services.AddSingleton<ITransactionCompletionTracker, TransactionCompletionTracker>();
        services.AddEasyNetQ(BuildConnectionString(rabbitSettings));
        services.AddHostedService<TransactionStatusSubscriberHostedService>();
        services.AddScoped<Transactions.Infrastructure.Messaging.Consumers.TransactionStatusConsumer>();

        return services;
    }

    private static string BuildConnectionString(RabbitMqSettings settings) {
        var virtualHost = string.IsNullOrWhiteSpace(settings.VirtualHost) ? "/" : settings.VirtualHost;

        return $"host={settings.Host};virtualHost={virtualHost};username={settings.Username};password={settings.Password}";
    }
}
