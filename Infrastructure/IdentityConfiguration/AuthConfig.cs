
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.IdentityConfiguration {
    public class AuthConfig {
        public const string ISSUER = "http://localhost:5000";

        public const string AUDIENCE = "http://localhost:5000";

        public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

        public const string KEY = "some-secret";

        public const int EXPIRE = 10;

        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(ASCIIEncoding.UTF8.GetBytes(KEY));
    }
}
