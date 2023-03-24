using domain.Entities;
using domain.Repositories.Contracts;
using service.Services.Categories.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.Services.Categories {
    public sealed class CategoryService : ICategoryService {
        private readonly IRepositoriesFactory _categoryRepositoriesFactory;

        public CategoryService(IRepositoriesFactory categoryRepositoriesFactory) {
            _categoryRepositoriesFactory = categoryRepositoriesFactory;
        }

        public Task<List<Category>> AddCategoryAsync(Category category) {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository();

                categoriesRepository.Add(category);

                return categoriesRepository.GetAll();
            });
        }

        public Task DeleteCategoryAsync(Category category) {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository();

                categoriesRepository.Delete(category);
            });
        }

        public Task<List<Category>> GetAllCategoriesAsync() {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository();

                return categoriesRepository.GetAll();
            });
        }

        public Task<Category> GetCategoryByIdAsync(long id) {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository();

                return categoriesRepository.GetById(id);
            });
        }

        public Task<Category> UpdateCategoryAsync(Category category) {
            throw new NotImplementedException();
        }
    }
}
