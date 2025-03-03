using domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database {
    public sealed class ApplicationIdentityContext : IdentityDbContext<User> {
        public ApplicationIdentityContext(DbContextOptions<ApplicationIdentityContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
