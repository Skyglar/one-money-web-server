using Finances.Domain.SeedWork;

namespace Finances.Domain.AggregateModels.CategoryAggregate;

public interface ICategoryRepository : IRepository<Category> {
    Task<Category> AddAsync(Category category, CancellationToken ct = default);

    void Update(Category category);

    Task<Category> GetAsync(int categoryId, CancellationToken ct = default);
}