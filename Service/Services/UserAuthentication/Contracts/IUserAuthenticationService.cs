using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using service.Dtos;

namespace service.Services.UserAuthentication.Contracts {
    public interface IUserAuthenticationService {
        Task<IdentityResult> SignUpUserAsync(UserRegistrationDto userDto);

        Task<object> RequestTokenAsync(UserLoginDto userDto);
    }
}
