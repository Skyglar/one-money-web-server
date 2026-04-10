using Finances.Domain.AggregateModels.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Infrastructure.EntityConfigurations;

internal class CurrencyTypeEntityConfiguration : IEntityTypeConfiguration<Currency> {
    public void Configure(EntityTypeBuilder<Currency> builder) {
        builder.ToTable(nameof(Currency));
        
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

        builder.Property(a => a.Name).HasMaxLength(50);

        builder.Property(a => a.Code).HasMaxLength(3);
    }
}