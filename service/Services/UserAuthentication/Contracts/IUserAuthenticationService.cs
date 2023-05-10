
using domain.Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace service.Services.UserAuthentication.Contracts {
    public interface IUserAuthenticationService {
        Task<IdentityResult> SignUpUserAsync(UserRegistrationDto userDto);

        Task<object> RequestTokenAsync(UserLoginDto userDto);
    }
}
