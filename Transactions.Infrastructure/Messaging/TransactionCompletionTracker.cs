using System.Collections.Concurrent;
using OneMoney.Common.Primitives;

namespace Transactions.Infrastructure.Messaging;

public sealed class TransactionCompletionTracker : ITransactionCompletionTracker {
    public const string TimeoutMessage = "The operation timed out.";
    public const string AbortedMessage = "The operation was aborted.";

    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<Result>> _pending = new();

    public void Register(Guid transactionId) {
        var tcs = new TaskCompletionSource<Result>(TaskCreationOptions.RunContinuationsAsynchronously);
        if (!_pending.TryAdd(transactionId, tcs)) {
            throw new InvalidOperationException($"A completion is already registered for transaction {transactionId}.");
        }
    }

    public void Unregister(Guid transactionId) {
        if (_pending.TryRemove(transactionId, out var tcs)) {
            tcs.TrySetResult(Result.Failure(AbortedMessage));
        }
    }

    public async Task<Result> WaitAsync(Guid transactionId, TimeSpan timeout, CancellationToken cancellationToken) {
        if (!_pending.TryGetValue(transactionId, out var tcs)) {
            return Result.Failure("No completion was registered for this transaction.");
        }

        try {
            return await tcs.Task.WaitAsync(timeout, cancellationToken).ConfigureAwait(false);
        }
        catch (TimeoutException) {
            if (_pending.TryRemove(transactionId, out var pending)) {
                pending.TrySetResult(Result.Failure(TimeoutMessage));
            }

            return Result.Failure(TimeoutMessage);
        }
    }

    public void CompleteSuccess(Guid transactionId) {
        if (_pending.TryRemove(transactionId, out var tcs)) {
            tcs.TrySetResult(Result.Success());
        }
    }

    public void CompleteFailure(Guid transactionId, string error) {
        if (_pending.TryRemove(transactionId, out var tcs)) {
            tcs.TrySetResult(Result.Failure(error));
        }
    }
}
