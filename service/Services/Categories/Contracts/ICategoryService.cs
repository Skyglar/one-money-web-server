
using domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.Services.Categories.Contracts {
    public interface ICategoryService {
        Task<List<Category>> AddCategoryAsync(Category category);

        Task<Category> UpdateCategoryAsync(Category category);

        Task DeleteCategoryAsync(Category category);

        Task<Category> GetCategoryByIdAsync(long id);

        Task<List<Category>> GetAllCategoriesAsync();
    }
}
