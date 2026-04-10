using Finances.Domain.AggregateModels.CategoryAggregate;

namespace Finances.Infrastructure.Repositories;

public sealed class CategoryRepository : ICategoryRepository {
    public Category Add(Category category) {
        throw new NotImplementedException();
    }
    public void Update(Category category) {
        throw new NotImplementedException();
    }
    public Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken ct = default) {
        throw new NotImplementedException();
    }
}