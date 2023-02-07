
using domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.Repositories.Contracts {
    public interface ICategoriesRepository {
        Task Add(Category category);

        Task<List<Category>> GetAll();
    }
}
