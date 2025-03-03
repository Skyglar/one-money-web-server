
using common.Attributes.DILifeTimeAttributes;
using Infrastructure.Database;
using domain.Entities;
using Infrastructure.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories {
    [ScopedRegistration]
    public sealed class CategoryRepository : ICategoryRepository {
        private readonly OneMoneyContext _dbContext;

        public CategoryRepository(OneMoneyContext dbContext) {
            _dbContext = dbContext;
        }

        public void Add(Category category) {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void Delete(Category category) {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }

        public List<Category> GetAll() {
            return _dbContext.Categories.ToList();
        }

        public Category GetById(long id) {
            return _dbContext.Categories.Find(id);
        }

        public Category UpdateCategory(Category category) {
            Category updated = _dbContext.Categories.Update(category).Entity;
            _dbContext.SaveChanges();
            return updated;
        }
    }
}
