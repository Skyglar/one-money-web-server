
using common.Attributes.DILifeTimeAttributes;
using domain.Entities;
using Infrastructure.IdentityConfiguration;
using Infrastructure.Repositories.UserIdentityRepositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UserIdentityRepositories {
    [ScopedRegistration]
    public sealed class UserAuthenticationRepository : IUserAuthenticationRepository {
        private readonly UserManager<User> userManager;

        public UserAuthenticationRepository(UserManager<User> userManager) {
            this.userManager = userManager;
        }


        public async Task<IdentityResult> SignUpAsync(User user, string password) {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserClaimsAsync(User user) {
            List<Claim> claims = new List<Claim> {
                new Claim("NetId", user.NetId.ToString())
            };

            return await this.userManager.AddClaimsAsync(user, claims);
        }

        public async Task<Tuple<string, JwtSecurityToken>> ValidateUserAndGetClaimsAsync(string userEmail, string password) {
            User user = await this.userManager.FindByEmailAsync(userEmail);

            if (user == null) 
                return new Tuple<string, JwtSecurityToken>("Error message, create constant for errors", null);

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
                return new Tuple<string, JwtSecurityToken>("Error message, create constant for errors", null);

            SymmetricSecurityKey key = AuthConfig.GetSymmetricSecurityKey();
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim> {
                new Claim("NetId", user.NetId.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: AuthConfig.ISSUER, 
                audience: AuthConfig.AUDIENCE, 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(AuthConfig.EXPIRE)), 
                signingCredentials: signingCredentials);

            return  new Tuple<string, JwtSecurityToken>(string.Empty, token); ;
        }
    }
}
