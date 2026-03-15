
using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.AccountAggregate;

public interface ICurrencyRepository : IRepository<Currency> {
    Task<Currency> FindByCodeAsync(string code, CancellationToken ct = default);
    Task<Currency> FindByIdAsync(int id, CancellationToken ct = default);
}