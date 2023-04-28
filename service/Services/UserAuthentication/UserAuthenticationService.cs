
using common.Attributes.DILifeTimeAttributes;
using domain.Entities;
using domain.Entities.Dtos;
using domain.Repositories.UserIdentiyRepositories.Contracts;
using service.Services.UserAuthentication.Contracts;
using System.Threading.Tasks;

namespace service.Services.UserAuthentication {
    [ScopedRegistration]
    public sealed class UserAuthenticationService : IUserAuthenticationService {
        private readonly IUserAuthenticationRepository userAuthenticationRepository;

        public UserAuthenticationService(IUserAuthenticationRepository userAuthenticationRepository) {
            this.userAuthenticationRepository = userAuthenticationRepository;
        }

        public async Task SignUpUserAsync(UserRegistrationDto userDto) {
            await userAuthenticationRepository.SignUpAsync(new User {
                Email = userDto.Email,
                UserName = userDto.UserName
            },
            userDto.Password);
        }
    }
}
