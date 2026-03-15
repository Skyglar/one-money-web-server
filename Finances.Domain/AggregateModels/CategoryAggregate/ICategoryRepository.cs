using OneMoney.Common.SeedWork;

namespace Finances.Domain.AggregateModels.CategoryAggregate;

public interface ICategoryRepository : IRepository<Category> {
    Category Add(Category category);

    void Update(Category category);

    Task<Category> GetAsync(int categoryId, CancellationToken ct = default);
}