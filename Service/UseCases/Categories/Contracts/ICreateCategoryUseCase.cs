using System.Threading.Tasks;
using service.Responses.Categories;
using domain.Entities;

namespace service.UseCases.Categories.Contracts;

public interface ICreateCategoryUseCase {
    Task<CategoryCreatedResponse> CreateCategory(Category category);
}