using Finances.Domain.AggregateModels.AccountAggregate;
using Finances.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Finances.Infrastructure.Repositories;

public sealed class AccountRepository(FinancesDbContext context) : IAccountRepository {
    public void Add(Account account) => context.Set<Account>().Add(account);
    
    public void Update(Account account) => context.Set<Account>().Update(account);
    
    public async Task<Account?> GetByExternalIdAsync(Guid accountIdentityGuid, CancellationToken ct = default) =>
       await context.Set<Account>().FirstOrDefaultAsync(a => a.Id.Equals(accountIdentityGuid), ct);

    public async Task<Account?> GetByInternalIdAsync(long id, CancellationToken ct = default) =>
       await context.Set<Account>().FirstOrDefaultAsync(a => a.Id.Equals(id), ct);

    public Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid userId, CancellationToken ct = default) {
        throw new NotImplementedException();
    }
}