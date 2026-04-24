using EasyNetQ;
using Finances.Application.Commands;
using MediatR;
using OneMoney.Common.Events;

namespace Finances.Infrastructure.Messaging.Consumers;

public sealed class TransactionCreatedConsumer(IMediator mediator, IBus bus) {
    public async Task HandleAsync(TransactionCreatedIntegrationEvent message, CancellationToken cancellationToken) {
        var command = new ProcessIncomingTransactionCommand(
            message.TransactionId,
            message.AccountId,
            message.CategoryId,
            message.Amount);

        var result = await mediator.Send(command, cancellationToken);

        if (result.IsFailure) {
            await bus.PubSub.PublishAsync(new TransactionFailedIntegrationEvent(
                message.TransactionId,
                result.Error
            ), cancellationToken);
            return;
        }

        await bus.PubSub.PublishAsync(new TransactionSuccessIntegrationEvent(message.TransactionId), cancellationToken);
    }
}
