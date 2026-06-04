using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.CategoryAggregate;

public interface ICategoryRepository : IRepository<Category> {
    void Add(Category category);

    void Update(Category category);

    Task<Category?> GetByInternalIdAsync(long id, CancellationToken ct = default);
    
    Task<Category?> GetByExternalIdAsync(Guid externalId, CancellationToken ct = default);
}