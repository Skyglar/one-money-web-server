
using common.Attributes.DILifeTimeAttributes;
using domain.Database;
using domain.Entities;
using domain.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace domain.Repositories {
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
        }

        public List<Category> GetAll() {
            return _dbContext.Categories.ToList();
        }

        public Category GetById(long id) {
            return _dbContext.Categories.Find(id);
        }
    }
}
