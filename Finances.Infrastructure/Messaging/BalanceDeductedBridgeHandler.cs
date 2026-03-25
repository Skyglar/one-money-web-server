using Finances.Domain.Events;
using MassTransit;
using MediatR;
using OneMoney.Common.Events;

namespace Finances.Infrastructure.Messaging;

public sealed class BalanceDeductedBridgeHandler(IPublishEndpoint publish) : INotificationHandler<BalanceDeductedDomainEvent> {
    public async Task Handle(BalanceDeductedDomainEvent notification, CancellationToken cancellationToken) {
        await publish.Publish(new TransactionSuccessIntegrationEvent(notification.TransactionId), cancellationToken);
    }
}