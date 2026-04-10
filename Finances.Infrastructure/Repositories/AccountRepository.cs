using Finances.Domain.AggregateModels.AccountAggregate;

namespace Finances.Infrastructure.Repositories;

public sealed class AccountRepository : IAccountRepository {
    public void Add(Account account) {
        throw new NotImplementedException();
    }
    public void Update(Account account) {
        throw new NotImplementedException();
    }
    public Task<Account?> FindAsync(string accountIdentityGuid, CancellationToken ct = default) {
        throw new NotImplementedException();
    }
    public Task<Account?> GetByIdAsync(Guid id, CancellationToken ct = default) {
        throw new NotImplementedException();
    }
}