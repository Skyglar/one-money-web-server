using domain.Entities;
using Infrastructure.Repositories.Contracts;
using service.Services.Categories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.Services.Categories {
    public sealed class CategoryService : ICategoryService {
        private readonly ICategoryRepository _categoriesRepository;

        public CategoryService(ICategoryRepository categoriesRepository) {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<List<Category>> AddCategoryAsync(Category category) {
            await _categoriesRepository.AddAsync(category);

            return await _categoriesRepository.GetAllAsync();
        }

        public async Task DeleteCategoryAsync(Category category) {
            _categoriesRepository.Delete(category);
        }

        public async Task<List<Category>> GetAllCategoriesAsync() {
            return await _categoriesRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(long id) {
            return await _categoriesRepository.GetByIdAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(Category category) {
            _categoriesRepository.UpdateCategory(category);
            return await GetCategoryByIdAsync(category.Id);
        }
    }
}