
using Infrastructure.Database;
using domain.Entities;
using Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories {
    public sealed class CategoryRepository : ICategoryRepository {
        private readonly OneMoneyContext _dbContext;

        public CategoryRepository(OneMoneyContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken = default) {
            await _dbContext.Categories.AddAsync(category, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default) {
            return await _dbContext.Categories.ToListAsync(cancellationToken);
        }
        
        public async Task<Category?> GetByIdAsync(long id, CancellationToken ct = default) {
            return await _dbContext.Categories.FindAsync(new object[] { id }, ct);
        }
        
        public void Delete(Category category) {
            _dbContext.Categories.Remove(category);
            // Warning: If you save here, it makes it hard to do multi-table transactions.
            _dbContext.SaveChanges(); 
        }
        
        public void UpdateCategory(Category category) {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
        }
    }
}
