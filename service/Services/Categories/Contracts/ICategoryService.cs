
using domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.Services.Categories.Contracts {
    public interface ICategoryService {
        Task<List<Category>> AddCategoryAsync(Category category);

        Task<Category> UpdateCategoryAsync(Category category);

        Task DeleteCategoryAsync(string id);

        Task<Category> GetCategoryByIdAsync(string id);

        Task<List<Category>> GetAllCategoriesAsync();
    }
}
