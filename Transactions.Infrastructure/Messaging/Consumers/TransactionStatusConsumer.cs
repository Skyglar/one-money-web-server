using MassTransit;
using OneMoney.Common.Events;

namespace Transactions.Infrastructure.Messaging.Consumers;

public sealed class TransactionStatusConsumer : 
    IConsumer<TransactionSuccessIntegrationEvent>, 
    IConsumer<TransactionFailedIntegrationEvent> {
    // TODO Implement 
    public Task Consume(ConsumeContext<TransactionSuccessIntegrationEvent> context) {
        throw new NotImplementedException();
    }
    public Task Consume(ConsumeContext<TransactionFailedIntegrationEvent> context) {
        throw new NotImplementedException();
    }
}