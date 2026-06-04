
using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public interface IAccountRepository : IRepository<Account> {
    void Add(Account account);
    
    void Update(Account account);

    Task<Account?> GetByExternalIdAsync(Guid accountIdentityGuid, CancellationToken ct = default);
    
    Task<Account?> GetByInternalIdAsync(long id, CancellationToken ct = default);
    
    Task<IEnumerable<Account>> GetAllByUserIdAsync(Guid userId, CancellationToken ct = default);
}