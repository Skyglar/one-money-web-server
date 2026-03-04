
using domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Repositories.Contracts {
    public interface ICategoryRepository {
        Task AddAsync(Category category, CancellationToken cancellationToken = default);

        Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Category?> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        void Delete(Category category);

        void UpdateCategory(Category category);
    }
}
