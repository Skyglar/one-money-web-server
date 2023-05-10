
using common.Attributes.DILifeTimeAttributes;
using domain.Entities;
using domain.Entities.Dtos;
using domain.IdentityConfiguration;
using domain.Repositories.UserIdentiyRepositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using service.Services.UserAuthentication.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.UserAuthentication
{
    [ScopedRegistration]
    public sealed class UserAuthenticationService : IUserAuthenticationService {
        private readonly IUserAuthenticationRepository userAuthenticationRepository;

        public UserAuthenticationService(IUserAuthenticationRepository userAuthenticationRepository) {
            this.userAuthenticationRepository = userAuthenticationRepository;
        }

        public async Task<object> RequestTokenAsync(UserLoginDto userDto) {

            (string errorMessage, JwtSecurityToken token) = 
                await userAuthenticationRepository.ValidateUserAndGetClaimsAsync(userDto.Email, userDto.Password);


            return null;
        }

        public async Task<IdentityResult> SignUpUserAsync(UserRegistrationDto userDto) {
            User user = new User {
                UserName = userDto.UserName,
                Email = userDto.Email,
            };

            string passwordHash = new PasswordHasher<User>().HashPassword(user, userDto.Password);

            IdentityResult identityResult = await userAuthenticationRepository.SignUpAsync(user, passwordHash);

            if (identityResult.Succeeded) {
                await userAuthenticationRepository.AddUserClaimsAsync(user);
            }

            return identityResult;
        }
    }
}
