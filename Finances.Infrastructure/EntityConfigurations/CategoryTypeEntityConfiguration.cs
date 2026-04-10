using Finances.Domain.AggregateModels.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Infrastructure.EntityConfigurations;

internal class CategoryTypeEntityConfiguration : IEntityTypeConfiguration<Category> {
    public void Configure(EntityTypeBuilder<Category> builder) {
        builder.ToTable(nameof(Category));
        
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

        builder.Property(a => a.Image).HasMaxLength(300);

        builder.Property(a => a.Color).HasMaxLength(10);
    }
}