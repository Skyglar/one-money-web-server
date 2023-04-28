
using domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace domain.Repositories.UserIdentiyRepositories.Contracts {
    public interface IUserAuthenticationRepository {
        Task<IdentityResult> SignUpAsync(User user, string password);
    }
}
