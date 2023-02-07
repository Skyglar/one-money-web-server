
using domain.Entities;
using System.Collections.Generic;

namespace domain.Repositories.Contracts {
    public interface ICategoriesRepository {
        void Add(Category category);

        List<Category> GetAll();

        Category GetById(string id);

        void Delete(string id);
    }
}
