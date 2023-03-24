

namespace domain.Repositories.Contracts {
    public interface IRepositoriesFactory {
        ICategoriesRepository NewCategoryRepository();
    }
}
