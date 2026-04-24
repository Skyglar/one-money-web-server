using Microsoft.EntityFrameworkCore;
using Transactions.Domain.AggregateModels;
using Transactions.Infrastructure.Data;

namespace Transactions.Infrastructure.Repositories;

public sealed class TransactionRepository(TransactionsDbContext context) : ITransactionRepository {
    public void Add(Transaction transaction) => context.Set<Transaction>().Add(transaction);

    public void Update(Transaction transaction) => context.Set<Transaction>().Update(transaction);

    public Task<Transaction?> FindAsync(Guid id, CancellationToken ct = default) =>
        context.Set<Transaction>().FirstOrDefaultAsync(t => t.Id == id, ct);
}
