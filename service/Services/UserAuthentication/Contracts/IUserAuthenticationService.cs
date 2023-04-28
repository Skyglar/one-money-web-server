
using domain.Entities.Dtos;
using System.Threading.Tasks;

namespace service.Services.UserAuthentication.Contracts {
    public interface IUserAuthenticationService {
        Task SignUpUserAsync(UserRegistrationDto userDto);
    }
}
