using Transactions.Domain.AggregateModels;

namespace Transactions.Infrastructure.Repositories;

public sealed class TransactionRepository : ITransactionRepository {
    public void Add(Transaction transaction) {
        throw new NotImplementedException();
    }
    public void Update(Transaction transaction) {
        throw new NotImplementedException();
    }
    public Task<Transaction> FindAsync(Guid id, CancellationToken ct = default) {
        throw new NotImplementedException();
    }
}