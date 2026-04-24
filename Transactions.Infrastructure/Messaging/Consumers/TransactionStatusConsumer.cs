using OneMoney.Common.Events;
using OneMoney.Common.SeedWork;
using Transactions.Domain.AggregateModels;

namespace Transactions.Infrastructure.Messaging.Consumers;

public sealed class TransactionStatusConsumer(
    ITransactionCompletionTracker completionTracker,
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork) {

    public async Task HandleSuccessAsync(TransactionSuccessIntegrationEvent message, CancellationToken cancellationToken) {
        completionTracker.CompleteSuccess(message.TransactionId);
        await TryPersistStatusAsync(message.TransactionId, completed: true, failureReason: null, cancellationToken);
    }

    public async Task HandleFailureAsync(TransactionFailedIntegrationEvent message, CancellationToken cancellationToken) {
        completionTracker.CompleteFailure(message.TransactionId, message.Error);
        await TryPersistStatusAsync(message.TransactionId, completed: false, failureReason: message.Error, cancellationToken);
    }

    private async Task TryPersistStatusAsync(Guid transactionId, bool completed, string? failureReason, CancellationToken cancellationToken) {
        var entity = await transactionRepository.FindAsync(transactionId, cancellationToken);
        if (entity is null) {
            return;
        }

        if (completed) {
            entity.MarkAsCompleted();
        }
        else {
            entity.MarkAsFailed(failureReason ?? "Unknown error");
        }

        transactionRepository.Update(entity);
        await unitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
