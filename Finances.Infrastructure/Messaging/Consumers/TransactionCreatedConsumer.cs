using Finances.Application.Commands;
using MassTransit;
using MediatR;
using OneMoney.Common.Events;

namespace Finances.Infrastructure.Messaging.Consumers;

public sealed class TransactionCreatedConsumer(IMediator mediator, IPublishEndpoint publishEndpoint) : IConsumer<TransactionCreatedIntegrationEvent> {
    public async Task Consume(ConsumeContext<TransactionCreatedIntegrationEvent> context) {
        var command = new ProcessIncomingTransactionCommand(
            context.Message.TransactionId, 
            context.Message.AccountId,
            context.Message.CategoryId,
            context.Message.Amount);
    
        var result = await mediator.Send(command);

        if (result.IsFailure) 
        {
            // TODO Add logger
            // _logger.LogWarning("Transaction {Id} failed: {Error}", context.Message.Id, result.Error);

            await publishEndpoint.Publish(new TransactionFailedIntegrationEvent(
                context.Message.TransactionId, 
                result.Error
            ));
        }
    }
}