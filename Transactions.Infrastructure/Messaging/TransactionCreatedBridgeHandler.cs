using EasyNetQ;
using MediatR;
using OneMoney.Common.Events;
using Transactions.Domain.Events;

namespace Transactions.Infrastructure.Messaging;

public sealed class TransactionCreatedBridgeHandler(IBus bus) : INotificationHandler<TransactionCreatedDomainEvent> {
    public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken) {
        var integrationEvent = new TransactionCreatedIntegrationEvent(
            notification.Amount,
            notification.AccountId,
            notification.CategoryId,
            notification.TransactionId,
            DateTime.UtcNow);

        await bus.PubSub.PublishAsync(integrationEvent, cancellationToken);
    }
}
