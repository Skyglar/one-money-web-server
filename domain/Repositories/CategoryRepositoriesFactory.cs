
using domain.Repositories.Contracts;
using MongoDB.Driver;

namespace domain.Repositories {
    public sealed class CategoryRepositoriesFactory : ICategoryRepositoriesFactory {
        public ICategoriesRepository NewCategoryRepository(IMongoDatabase connection) {
            return new CategoryRepository(connection);
        }
    }
}
