
using domain.Entities;
using domain.Repositories.Contracts;
using MongoDB.Driver;
using System.Collections.Generic;

namespace domain.Repositories {
    public sealed class CategoryRepository : ICategoriesRepository {
        private readonly IMongoCollection<Category> _categoriesCollection;

        public CategoryRepository(IMongoDatabase connection) {
            _categoriesCollection = connection.GetCollection<Category>(nameof(Category));
        }

        public void Add(Category category) {
            _categoriesCollection.InsertOne(category);
        }

        public void Delete(string id) {
            _categoriesCollection.DeleteOne(id);
        }

        public List<Category> GetAll() {
            return _categoriesCollection.Find(c => true).ToList();
        }

        public Category GetById(string id) {
            return _categoriesCollection.Find(c => c.Id.Equals(id)).FirstOrDefault();
        }
    }
}
