
using MongoDB.Driver;

namespace domain.Repositories.Contracts {
    public interface ICategoryRepositoriesFactory {
        ICategoriesRepository NewCategoryRepository(IMongoDatabase connection);
    }
}
