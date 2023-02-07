using domain.Entities;
using domain.Repositories.Contracts;
using domain.Repositories.DbConnection.Contracts;
using service.Services.Categories.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace service.Services.Categories {
    public sealed class CategoryService : ICategoryService {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ICategoryRepositoriesFactory _categoryRepositoriesFactory;

        public CategoryService(IDbConnectionFactory connectionFactory, ICategoryRepositoriesFactory categoryRepositoriesFactory) {
            _connectionFactory = connectionFactory;
            _categoryRepositoriesFactory = categoryRepositoriesFactory;
        }

        public Task<List<Category>> AddCategoryAsync(Category category) {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository(_connectionFactory.NewDatabaseConnection());

                categoriesRepository.Add(category);

                return categoriesRepository.GetAll();
            });
        }

        public Task DeleteCategoryAsync(string id) {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository(_connectionFactory.NewDatabaseConnection());

                categoriesRepository.Delete(id);
            });
        }

        public Task<List<Category>> GetAllCategoriesAsync() {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository(_connectionFactory.NewDatabaseConnection());

                return categoriesRepository.GetAll();
            });
        }

        public Task<Category> GetCategoryByIdAsync(string id) {
            return Task.Run(() => {
                ICategoriesRepository categoriesRepository =
                    _categoryRepositoriesFactory.NewCategoryRepository(_connectionFactory.NewDatabaseConnection());

                return categoriesRepository.GetById(id);
            });
        }

        public Task<Category> UpdateCategoryAsync(Category category) {
            throw new NotImplementedException();
        }
    }
}
