using common.Attributes.DILifeTimeAttributes;
using domain.Entities;
using domain.Repositories.Contracts;
using service.Services.Categories.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.Services.Categories {
    [ScopedRegistration]
    public sealed class CategoryService : ICategoryService {
        private readonly ICategoryRepository _categoriesRepository;

        public CategoryService(ICategoryRepository categoriesRepository) {
            _categoriesRepository = categoriesRepository;
        }

        public Task<List<Category>> AddCategoryAsync(Category category) {
            return Task.Run(() => {
                _categoriesRepository.Add(category);

                return _categoriesRepository.GetAll();
            });
        }

        public Task DeleteCategoryAsync(Category category) {
            return Task.Run(() => {
                _categoriesRepository.Delete(category);
            });
        }

        public Task<List<Category>> GetAllCategoriesAsync() {
            return Task.Run(() => {
                return _categoriesRepository.GetAll();
            });
        }

        public Task<Category> GetCategoryByIdAsync(long id) {
            return Task.Run(() => {
                return _categoriesRepository.GetById(id);
            });
        }

        public Task<Category> UpdateCategoryAsync(Category category) {
            throw new NotImplementedException();
        }
    }
}
