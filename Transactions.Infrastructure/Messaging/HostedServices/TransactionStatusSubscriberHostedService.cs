using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneMoney.Common.Events;
using Transactions.Infrastructure.Messaging.Consumers;

namespace Transactions.Infrastructure.Messaging.HostedServices;

public sealed class TransactionStatusSubscriberHostedService(
    IBus bus,
    IServiceScopeFactory scopeFactory) : IHostedService {

    public async Task StartAsync(CancellationToken cancellationToken) {
        await bus.PubSub.SubscribeAsync<TransactionSuccessIntegrationEvent>(
            subscriptionId: "transactions.status.success.v1",
            onMessage: async (message, ct) => {
                using var scope = scopeFactory.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<TransactionStatusConsumer>();
                await consumer.HandleSuccessAsync(message, ct);
            },
            configure: _ => { },
            cancellationToken: cancellationToken);

        await bus.PubSub.SubscribeAsync<TransactionFailedIntegrationEvent>(
            subscriptionId: "transactions.status.failed.v1",
            onMessage: async (message, ct) => {
                using var scope = scopeFactory.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<TransactionStatusConsumer>();
                await consumer.HandleFailureAsync(message, ct);
            },
            configure: _ => { },
            cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
