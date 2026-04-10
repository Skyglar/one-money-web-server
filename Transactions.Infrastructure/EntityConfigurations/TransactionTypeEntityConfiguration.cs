using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transactions.Domain.AggregateModels;

namespace Transactions.Infrastructure.EntityConfigurations;

public class TransactionTypeEntityConfiguration : IEntityTypeConfiguration<Transaction> {
    public void Configure(EntityTypeBuilder<Transaction> builder) {
        builder.ToTable(nameof(Transaction));
        
        builder.Ignore(e => e.DomainEvents);
        
        builder.HasKey(a => a.InternalId)
            .IsClustered();

        builder.Property(a => a.InternalId)
            .UseIdentityColumn();

        builder.Property(a => a.Id)
            .IsRequired();

        builder.HasIndex(a => a.Id)
            .IsUnique();

        builder.Property(t => t.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.CurrencyCode)
            .HasMaxLength(3)
            .IsRequired();
        
        builder.Property(a => a.Status)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(a => a.Description).HasMaxLength(300);

        builder.Property(a => a.FailureReason)
            .HasMaxLength(300)
            .IsRequired(false);
    }
}