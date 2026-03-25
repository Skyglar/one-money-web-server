using MassTransit;
using MediatR;
using OneMoney.Common.Events;
using Transactions.Domain.Events;

namespace Transactions.Infrastructure.Messaging;

public sealed class TransactionCreatedBridgeHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<TransactionCreatedDomainEvent> {
    public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken) {
        var integrationEvent = new TransactionCreatedIntegrationEvent(
            notification.Amount,
            notification.AccountId,
            notification.CategoryId,
            notification.TransactionId,
            DateTime.UtcNow);
        
        await publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}