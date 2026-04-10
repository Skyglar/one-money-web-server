using Finances.Domain.AggregateModels.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Infrastructure.EntityConfigurations;

internal class AccountTypeEntityConfiguration : IEntityTypeConfiguration<Account> {
    public void Configure(EntityTypeBuilder<Account> builder) {
        builder.ToTable(nameof(Account));

        builder.Ignore(e => e.DomainEvents);

        builder.HasKey(a => a.InternalId)
            .IsClustered();

        builder.Property(a => a.InternalId)
            .UseIdentityColumn();

        builder.Property(a => a.Id)
            .IsRequired();

        builder.HasIndex(a => a.Id)
            .IsUnique();

        builder.Property(a => a.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.Description).HasMaxLength(200);

        builder.Property(a => a.Balance)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(a => a.AccountType)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.HasOne(a => a.Currency)
            .WithMany()
            .HasForeignKey(a => a.CurrencyId)
            .IsRequired();
    }
}