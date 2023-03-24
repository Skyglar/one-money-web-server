
using common;
using domain.Database;
using domain.Repositories.Contracts;

namespace domain.Repositories {
    public sealed class RepositoriesFactory : IRepositoriesFactory {
        public ICategoriesRepository NewCategoryRepository() =>
            new CategoryRepository(new OneMoneyContext(AppSettingsConfigurationManager.DatabaseName));
    }
}
