using domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Repositories.UserIdentityRepositories.Contracts
{
    public interface IUserAuthenticationRepository
    {
        Task<IdentityResult> SignUpAsync(User user, string password);

        Task<IdentityResult> AddUserClaimsAsync(User user);

        Task<Tuple<string, JwtSecurityToken>> ValidateUserAndGetClaimsAsync(string userEmail, string password);
    }
}
