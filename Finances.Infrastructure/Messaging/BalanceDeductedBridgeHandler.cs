using EasyNetQ;
using Finances.Domain.Events;
using MediatR;
using OneMoney.Common.Events;

namespace Finances.Infrastructure.Messaging;

public sealed class BalanceDeductedBridgeHandler(IBus bus) : INotificationHandler<BalanceDeductedDomainEvent> {
    public async Task Handle(BalanceDeductedDomainEvent notification, CancellationToken cancellationToken) {
        await bus.PubSub.PublishAsync(new TransactionSuccessIntegrationEvent(notification.TransactionId), cancellationToken);
    }
}
