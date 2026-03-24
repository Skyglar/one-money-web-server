using OneMoney.Common.SeedWork;

namespace Transactions.Domain.AggregateModels;

public interface ITransactionRepository : IRepository<Transaction> {
    void Add(Transaction transaction);
    
    void Update(Transaction transaction);
    
    Task<Transaction> FindAsync(Guid id, CancellationToken ct = default);
}