using Finances.Domain.AggregateModels.AccountAggregate;

namespace Finances.Infrastructure.Repositories;

public sealed class CurrencyRepository : ICurrencyRepository {
    public Task<Currency> FindByCodeAsync(string code, CancellationToken ct = default) {
        throw new NotImplementedException();
    }
    public Task<Currency> FindByIdAsync(int id, CancellationToken ct = default) {
        throw new NotImplementedException();
    }
}