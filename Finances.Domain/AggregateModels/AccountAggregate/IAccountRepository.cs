
using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public interface IAccountRepository : IRepository<Account> {
    void Add(Account account);
    
    void Update(Account account);

    Task<Account?> FindAsync(string accountIdentityGuid, CancellationToken ct = default);
    Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default);
}