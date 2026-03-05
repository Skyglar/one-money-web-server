using Finances.Domain.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public interface IAccountRepository : IRepository<Account> {
    Task<Account> AddAsync(Account account, CancellationToken ct = default);
    Account Update(Account account);
    Task<Account> FindAsync(string AccountIdentityGuid, CancellationToken ct = default);
    Task<Account> FindByIdAsync(int id, CancellationToken ct = default);
}