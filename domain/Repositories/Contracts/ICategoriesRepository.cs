
using domain.Entities;
using System.Collections.Generic;

namespace domain.Repositories.Contracts {
    public interface ICategoriesRepository {
        void Add(Category category);

        List<Category> GetAll();

        Category GetById(long id);

        void Delete(Category category);
    }
}
