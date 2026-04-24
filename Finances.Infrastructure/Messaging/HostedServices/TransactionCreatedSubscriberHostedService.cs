using EasyNetQ;
using Finances.Infrastructure.Messaging.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneMoney.Common.Events;

namespace Finances.Infrastructure.Messaging.HostedServices;

public sealed class TransactionCreatedSubscriberHostedService(
    IBus bus,
    IServiceScopeFactory scopeFactory) : IHostedService {

    public async Task StartAsync(CancellationToken cancellationToken) {
        await bus.PubSub.SubscribeAsync<TransactionCreatedIntegrationEvent>(
            subscriptionId: "finances.transaction.created.v1",
            onMessage: async (message, ct) => {
                using var scope = scopeFactory.CreateScope();
                var consumer = scope.ServiceProvider.GetRequiredService<TransactionCreatedConsumer>();
                await consumer.HandleAsync(message, ct);
            },
            configure: _ => { },
            cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
