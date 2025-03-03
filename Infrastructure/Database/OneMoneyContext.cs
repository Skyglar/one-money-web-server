using domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database {
    public class OneMoneyContext : DbContext {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }

        public OneMoneyContext(DbContextOptions<OneMoneyContext> options) : base(options) {

        }
    }
}
