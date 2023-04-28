
using common.Attributes.DILifeTimeAttributes;
using domain.Entities;
using domain.Repositories.UserIdentiyRepositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace domain.Repositories.UserIdentiyRepositories {
    [ScopedRegistration]
    public sealed class UserAutheticationRepository : IUserAuthenticationRepository {
        private readonly UserManager<User> userManager;

        public UserAutheticationRepository(UserManager<User> userManager) {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> SignUpAsync(User user, string password) {
            return await userManager.CreateAsync(user, password);
        }
    }
}
