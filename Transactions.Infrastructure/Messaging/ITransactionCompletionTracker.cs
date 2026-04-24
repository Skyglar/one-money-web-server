using OneMoney.Common.Primitives;

namespace Transactions.Infrastructure.Messaging;

public interface ITransactionCompletionTracker {
    void Register(Guid transactionId);
    void Unregister(Guid transactionId);
    Task<Result> WaitAsync(Guid transactionId, TimeSpan timeout, CancellationToken cancellationToken);
    void CompleteSuccess(Guid transactionId);
    void CompleteFailure(Guid transactionId, string error);
}
