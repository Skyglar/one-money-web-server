
using domain.Entities;
using domain.Repositories.Contracts;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Repositories {
    public sealed class CategoryRepository : ICategoriesRepository {
        private readonly IMongoCollection<Category> _categoriesCollection;

        public CategoryRepository(IMongoDatabase connection) {
            _categoriesCollection = connection.GetCollection<Category>(nameof(Category));
        }

        public async Task Add(Category category) {
            await _categoriesCollection.InsertOneAsync(category);
        }

        public async Task<List<Category>> GetAll() {
            return await _categoriesCollection.Find(c => true).ToListAsync();
        }
    }
}
