using Finances.Domain.AggregateModels.CategoryAggregate;
using Finances.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Finances.Infrastructure.Repositories;

public sealed class CategoryRepository(FinancesDbContext context) : ICategoryRepository {
    public void Add(Category category) => context.Set<Category>().Add(category);
    public void Update(Category category) => context.Set<Category>().Update(category);
    
    public async Task<Category?> GetByInternalIdAsync(long id, CancellationToken ct = default) =>
        await context.Set<Category>().FirstOrDefaultAsync(c => c.InternalId.Equals(id), ct);

    public Task<Category?> GetByExternalIdAsync(Guid externalId, CancellationToken ct = default) =>
        context.Set<Category>().FirstOrDefaultAsync(c => c.Id.Equals(externalId), ct);
}