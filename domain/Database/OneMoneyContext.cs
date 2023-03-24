using domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace domain.Database {
    public sealed class OneMoneyContext : DbContext {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public OneMoneyContext(DbContextOptions<OneMoneyContext> options) : base(options) {

        }
    }
}
